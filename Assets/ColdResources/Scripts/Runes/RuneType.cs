using System;
using UnityEngine;

public enum RuneType
{
    None = 0,
    Water = 1,
    Fire = 2,
    Wood = 3,
    Stone = 4
}

public class Rune
{
    public RuneType Type;
    public float birthTime;
    public bool IsActive, IsFading;

    public Rune(RuneType type)
    {
        if (type == RuneType.None)
            throw new Exception("Rune was initialized with a None type");

        Type = type;
        birthTime = Time.time;
        IsActive = true;
        IsFading = false;
    }
}