using System;
using System.Collections.Generic;

public class DiceParser
{
    public static List<Dice> Parse(string[] args)
    {
        if (args.Length < 3)
            throw new ArgumentException("Error: You must provide at least 3 dice configurations as command-line arguments.\nExample: dotnet run 2,2,4,4,9,9 1,1,6,6,8,8 3,3,5,5,7,7");

        var diceList = new List<Dice>();
        var diceSet = new HashSet<string>();

        foreach (var arg in args)
        {
            //if (!diceSet.Add(arg))
            //    throw new ArgumentException("Error: Duplicate dice configurations are not allowed.");

            var faces = arg.Split(',');
            if (faces.Length < 6)
                throw new ArgumentException($"Error: Each dice must have at least 6 faces. ");

            foreach (var face in faces)
            {
                if (!int.TryParse(face, out int result) || result < 0)
                    throw new ArgumentException($"Error: Dice faces must be non-negative integers. ");
            }

            diceList.Add(new Dice(Array.ConvertAll(faces, int.Parse)));
        }

        return diceList;
    }
}
