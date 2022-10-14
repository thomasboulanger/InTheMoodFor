using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RuneHandler : MonoBehaviour
{
    [SerializeField] WSInputReader _inputReader;
    [SerializeField] DemonHandler _demonHandler;
    public UnityEvent<Rune> OnRuneSpawned, OnRuneStartFading, OnRuneDespawned, OnRuneFailed;

    [SerializeField]
    float _runeLifetime = 20,
        _runeFadeDuration = 5;

    public List<Rune> ActiveRunes { get; private set; }

    private void Start()
    {
        ActiveRunes = new List<Rune>();
        _inputReader.OnRuneValidated += SpawnRune;
    }

    void OnDisable()
    {
        _inputReader.OnRuneValidated -= SpawnRune;
    }

    private void Update()
    {
        UpdateRunes();
    }

    public void SpawnRune(RuneType runeType)
    {
        Rune rune = new Rune(runeType);
        bool success = _demonHandler.TryAddRune(rune);

        if (success)
        {
            rune.SetActive(true);
            ActiveRunes.Add(rune);
            OnRuneSpawned?.Invoke(rune);
        }
        else
        {
            OnRuneFailed?.Invoke(rune);
        }
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
                rune.SetFading(true);
                OnRuneStartFading?.Invoke(rune);
            }

            if (remainingLifetime <= 0f)
            {
                rune.SetActive(false);
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
