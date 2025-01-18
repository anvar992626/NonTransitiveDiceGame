using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length < 3)
        {
            Console.WriteLine("Error: You must provide at least 3 dice configurations as command-line arguments.");
            Console.WriteLine("Example: dotnet run 2,2,4,4,9,9 6,8,1,1,8,6 7,5,3,7,5,3");
            return;
        }

        try
        {
            var dice = DiceParser.Parse(args);

            var probabilities = ProbabilityCalculator.CalculateProbabilities(dice);
            HelpTable.Display(dice, probabilities);

            var game = new Game(dice);
            game.Start();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
