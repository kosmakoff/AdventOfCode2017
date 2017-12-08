using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;

namespace Day08
{
    internal class Instruction
    {
        private static readonly Regex InstructionRegex = new Regex(@"^(?<register>\w+) (?<op>inc|dec) (?<change>-?\d+) if (?<condRegister>\w+) (?<condOp><|>|<=|>=|!=|==) (?<condValue>-?\d+)$");

        internal enum Operation
        {
            Inc,
            Dec
        }

        internal enum Comparison
        {
            GT,
            GE,
            LT,
            LE,
            EQ,
            NEQ
        }

        private readonly string _registerName;

        private readonly Operation _operation;

        private readonly int _change;

        private readonly string _conditionRegister;

        private readonly Comparison _conditionOperation;

        private readonly int _conditionValue;

        public Instruction(string registerName, Operation operation, int change, string conditionRegister, Comparison conditionOperation, int conditionValue)
        {
            _registerName = registerName;
            _operation = operation;
            _change = change;
            _conditionRegister = conditionRegister;
            _conditionOperation = conditionOperation;
            _conditionValue = conditionValue;
        }

        public static Instruction Parse(string instructionString)
        {
            var match = InstructionRegex.Match(instructionString);
            if (!match.Success)
                throw new FormatException($"Instruction string '{instructionString}' was not recognized");

            return new Instruction(
                registerName: match.Groups["register"].Value,
                operation: ParseOperation(match.Groups["op"].Value),
                change: int.Parse(match.Groups["change"].Value),
                conditionRegister: match.Groups["condRegister"].Value,
                conditionOperation: ParseConditionOperation(match.Groups["condOp"].Value),
                conditionValue: int.Parse(match.Groups["condValue"].Value));
        }

        public void Execute(Memory mem)
        {
            var cond = BuildConditionExpression(mem);

            if (!cond())
                return;

            var value = mem.GetValue(_registerName);

            switch (_operation)
            {
                case Operation.Inc:
                    value += _change;
                    break;
                case Operation.Dec:
                    value -= _change;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            mem.SetValue(_registerName, value);
        }

        private Func<bool> BuildConditionExpression(Memory mem)
        {
            Func<Expression, Expression, BinaryExpression> binaryExpression;

            switch (_conditionOperation)
            {
                case Comparison.GT:
                    binaryExpression = Expression.GreaterThan;
                    break;
                case Comparison.GE:
                    binaryExpression = Expression.GreaterThanOrEqual;
                    break;
                case Comparison.LT:
                    binaryExpression = Expression.LessThan;
                    break;
                case Comparison.LE:
                    binaryExpression = Expression.LessThanOrEqual;
                    break;
                case Comparison.EQ:
                    binaryExpression = Expression.Equal;
                    break;
                case Comparison.NEQ:
                    binaryExpression = Expression.NotEqual;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return Expression
                .Lambda<Func<bool>>(
                    binaryExpression(
                        Expression.Constant(mem.GetValue(_conditionRegister)),
                        Expression.Constant(_conditionValue)))
                .Compile();
        }

        private static Comparison ParseConditionOperation(string condOpString)
        {
            switch (condOpString)
            {
                case ">":
                    return Comparison.GT;
                case "<":
                    return Comparison.LT;
                case ">=":
                    return Comparison.GE;
                case "<=":
                    return Comparison.LE;
                case "==":
                    return Comparison.EQ;
                case "!=":
                    return Comparison.NEQ;
                default:
                    throw new ArgumentOutOfRangeException(nameof(condOpString), condOpString, $"Conditional '{condOpString}' operator is not supported");
            }
        }

        private static Operation ParseOperation(string opString)
        {
            switch (opString)
            {
                case "inc":
                    return Operation.Inc;
                case "dec":
                    return Operation.Dec;
                default:
                    throw new ArgumentOutOfRangeException(nameof(opString), opString, "OpString must be inc or dec");
            }
        }
    }
}
