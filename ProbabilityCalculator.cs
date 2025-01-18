using System;
using System.Collections.Generic;

public class ProbabilityCalculator
{
    public static double[,] CalculateProbabilities(List<Dice> dice)
    {
        int n = dice.Count;
        var probabilities = new double[n, n];

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (i == j)
                {
                    probabilities[i, j] = -1; // Cannot play against itself
                }
                else
                {
                    probabilities[i, j] = GetProbability(dice[i], dice[j]);
                }
            }
        }
        return probabilities;
    }

    private static double GetProbability(Dice a, Dice b)
    {
        int wins = 0;
        int total = a.Faces.Length * b.Faces.Length;

        foreach (var faceA in a.Faces)
        {
            foreach (var faceB in b.Faces)
            {
                if (faceA > faceB)
                    wins++;
            }
        }
        return (double)wins / total;
    }
}
