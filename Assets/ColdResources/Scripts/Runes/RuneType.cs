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
    public bool IsActive { get; private set; }
    public bool IsFading { get; private set; }

    public event Action OnSpawn, OnDespawn, OnStartFading;

    public Rune(RuneType type)
    {
        if (type == RuneType.None)
            throw new Exception("Rune was initialized with a None type");

        Type = type;
        birthTime = Time.time;
        IsActive = true;
        IsFading = false;
    }

    public void SetActive(bool state)
    {
        IsActive = state;
        if (state) OnSpawn?.Invoke();
        else OnDespawn?.Invoke();
    }

    public void SetFading(bool state)
    {
        IsFading = state;
        if (state) OnStartFading?.Invoke();
    }


}