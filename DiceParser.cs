using System;
using System.Collections.Generic;

public class DiceParser
{
    public static List<Dice> Parse(string[] args)
    {
        if (args.Length < 3)
            throw new ArgumentException("You must provide at least 3 dice configurations.");

        var diceList = new List<Dice>();
        var diceSet = new HashSet<string>();

        foreach (var arg in args)
        {
            if (!diceSet.Add(arg))
                throw new ArgumentException($"Duplicate dice configuration found: {arg}. Each dice must be unique.");

            var faces = Array.ConvertAll(arg.Split(','), int.Parse);
            diceList.Add(new Dice(faces));
        }
        return diceList;
    }
}
