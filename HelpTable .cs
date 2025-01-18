using System;
using System.Collections.Generic;

public class HelpTable
{
    public static void Display(List<Dice> dice, double[,] probabilities)
    {
        Console.WriteLine("\nProbability Table");
        Console.WriteLine("Rows: User's dice, Columns: Computer's dice");
        Console.WriteLine("Each cell shows the probability of the user winning.\n");

        Console.Write("       ");
        for (int i = 0; i < dice.Count; i++)
            Console.Write($"  Dice {i}  ");
        Console.WriteLine();

        for (int i = 0; i < dice.Count; i++)
        {
            Console.Write($"Dice {i} ");
            for (int j = 0; j < dice.Count; j++)
            {
                if (probabilities[i, j] == -1)
                    Console.Write("    --    ");
                else
                    Console.Write($"  {probabilities[i, j]:0.0000} ");
            }
            Console.WriteLine();
        }

        Console.WriteLine("\nNote: The probabilities are calculated based on rolling all faces of both dice.");
        Console.WriteLine("Diagonal cells (--) indicate a dice cannot play against itself.");
    }
}
