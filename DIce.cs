using System;

public class Dice
{
    public int[] Faces { get; }

    public Dice(int[] faces)
    {
        if (faces.Length != 6)
            throw new ArgumentException("Each dice must have exactly 6 faces.");
        if (Array.Exists(faces, face => face < 0))
            throw new ArgumentException("Dice faces must be non-negative integers.");
        Faces = faces;
    }

    public int Roll(int index)
    {
        return Faces[index % Faces.Length];
    }
}
