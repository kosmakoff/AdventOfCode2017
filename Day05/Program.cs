using static Common.Utils;

namespace Day05
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            PrintHeader("Day 05");

            // var instructionsMemory = new InstructionsMemory(new[] { 0, 3, 0, 1, -3 }, isStrange: false);
            var instructionsMemory = new InstructionsMemory("Input.txt", isStrange: false);

            var steps = 0;

            while (!instructionsMemory.FollowInstruction())
                steps++;

            // count last step which led to exit
            steps++;

            PrintAnswer("Problem 1 answer", steps);

            // instructionsMemory = new InstructionsMemory(new[] { 0, 3, 0, 1, -3 }, isStrange: true);
            instructionsMemory = new InstructionsMemory("Input.txt", isStrange: true);

            steps = 0;

            while (!instructionsMemory.FollowInstruction())
                steps++;

            // count last step which led to exit
            steps++;

            PrintAnswer("Problem 2 answer", steps);
        }
    }
}
