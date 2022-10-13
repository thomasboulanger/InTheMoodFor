using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RuneHandler : MonoBehaviour
{
    [SerializeField] WSInputReader _inputReader;
    public UnityEvent<Rune> OnRuneSpawned, OnRuneStartFading, OnRuneDespawned;

    [SerializeField]
    float _runeLifetime = 20,
        _runeFadeDuration = 5;

    public List<Rune> ActiveRunes { get; private set; }

    private void Start()
    {
        ActiveRunes = new List<Rune>();
        _inputReader.OnRuneValidated += SpawnRune;
    }

    private void Update()
    {
        UpdateRunes();
    }

    public void SpawnRune(RuneType runeType)
    {
        Rune rune = new Rune(runeType);
        ActiveRunes.Add(rune);
        OnRuneSpawned?.Invoke(rune);
    }

    private void UpdateRunes()
    {
        int i = 0;
        float remainingLifetime;
        Rune rune;
        while (i < ActiveRunes.Count)
        {
            rune = ActiveRunes[i];
            remainingLifetime = rune.birthTime + _runeLifetime - Time.time;
            
            if (!rune.IsFading && remainingLifetime < _runeFadeDuration)
            {
                rune.IsFading = true;
                OnRuneStartFading?.Invoke(rune);
            }

            if (remainingLifetime <= 0f)
            {
                rune.IsActive = false;
                ActiveRunes.RemoveAt(i);
                OnRuneDespawned?.Invoke(rune);
            }
            else
            {
                i++;
            }
        }
    }
}
