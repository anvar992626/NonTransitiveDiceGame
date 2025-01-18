using System;
using System.Collections.Generic;

public class HelpTable
{
    public static void Display(List<Dice> dice, double[,] probabilities)
    {
        Console.WriteLine("\nProbability Table");
        Console.WriteLine("This table shows the probability of the user winning against the computer.");
        Console.WriteLine("Each row represents the user's dice, and each column represents the computer's dice.\n");

        Console.Write("       ");
        for (int i = 0; i < dice.Count; i++)
            Console.Write($"  Dice {i}  ");
        Console.WriteLine();

        for (int i = 0; i < dice.Count; i++)
        {
            Console.Write($"Dice {i} ");
            for (int j = 0; j < dice.Count; j++)
            {
                if (i == j)
                    Console.Write("    -     ");
                else
                    Console.Write($"  {probabilities[i, j]:0.0000} ");
            }
            Console.WriteLine();
        }
    }
}
