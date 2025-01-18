using System;

public class Dice
{
    public int[] Faces { get; }

    public Dice(int[] faces)
    {
        if (faces.Length < 2)
            throw new ArgumentException("Each dice must have at least 2 faces.");
        if (Array.Exists(faces, face => face < 0))
            throw new ArgumentException("Dice faces must be non-negative integers.");
        Faces = faces;
    }

    public int Roll(int index)
    {
        return Faces[index % Faces.Length];
    }
}
