using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            if (args.Length < 3)
            {
                Console.WriteLine("Error: You must provide at least 3 dice configurations as command-line arguments.");
                Console.WriteLine("Example: dotnet run 2,2,4,4,9,9 1,1,6,6,8,8 3,3,5,5,7,7");
                return;
            }

            var dice = DiceParser.Parse(args);

            // Start the game
            var game = new Game(dice);
            game.Start();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
