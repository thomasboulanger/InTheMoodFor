using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnSlot : MonoBehaviour
{
    public Rune Rune { get; private set; } = null;
    public DemonInfo Enemy { get; private set; }
    public Vector3 Position => transform.position;

    public UnityEvent OnRuneRemoved;
    public UnityEvent<RuneType> OnRuneAdded, OnRuneFading;

    public void AddEnemy(DemonInfo enemy)
    {
        Enemy = enemy;
        if (Rune != null && Rune.Type == Enemy.Type) Enemy.Ward();
    }

    public void AddRune(Rune rune)
    {
        if (Rune != null) Rune.OnStartFading -= RuneFading;
        Rune = rune;
        Rune.OnStartFading += RuneFading;

        if (Enemy != null && Rune.Type == Enemy.Type) Enemy.Ward();

        OnRuneAdded?.Invoke(Rune.Type);
    }

    public void RemoveRune()
    {
        if (Rune != null)
        {
            Rune.OnStartFading -= RuneFading;
            Rune = null;
        }
        Enemy?.Unward();

        OnRuneRemoved?.Invoke();
    }

    public void RuneFading()
    {
        Rune.OnStartFading -= RuneFading;
        OnRuneFading?.Invoke(Rune.Type);
    }
}
