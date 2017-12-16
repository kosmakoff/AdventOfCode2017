using System;

namespace Day16.Commands
{
    internal sealed class CommandParser
    {
        public ICommand Parse(string command)
        {
            var commandChar = command[0];

            switch (commandChar)
            {
                case 's':
                    var count = int.Parse(command.Substring(1));
                    return new SpinCommand(count);
                case 'x':
                    var exchangeParams = command.Substring(1).Split('/', StringSplitOptions.RemoveEmptyEntries);
                    var index1 = int.Parse(exchangeParams[0]);
                    var index2 = int.Parse(exchangeParams[1]);
                    return new ExchangeCommand(index1, index2);
                case 'p':
                    var @params = command.Substring(1).Split('/', StringSplitOptions.RemoveEmptyEntries);
                    var char1 = @params[0];
                    var char2 = @params[1];
                    return new PartnerCommand(char1[0], char2[0]);
                default:
                    throw new ArgumentOutOfRangeException(nameof(commandChar), $"Command not recognized: {command}");
            }
        }
    }
}
