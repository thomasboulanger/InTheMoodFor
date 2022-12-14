using NaughtyAttributes;
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "WS Input Reader")]
public class WSInputReader : ScriptableObject
{
    public Action<RuneType> OnRuneValidated;
    public Action OnRuneGone;

    public const string WATER = "eau";
    public const string FIRE = "feu";
    public const string WOOD = "bois";
    public const string STONE = "pierre";

    [SerializeField, BoxGroup("Validation params")]
    int _validationAmount = 20,
        _releaseAmount = 20,
        _validationBreakerAmount = 3;

    [SerializeField, BoxGroup("Running validation params"), ReadOnly]
    bool _isValidatingInput = false,
        _isWaitingForRelease = false;
    [SerializeField, BoxGroup("Running validation params"), ReadOnly]
    RuneType _bufferedRune;
    [SerializeField, BoxGroup("Running validation params"), ReadOnly]
    int _validationProgress = 0,
        _releaseProgress = 0,
        _validationBreakerProgress = 0;

    public void Init()
    {
        _bufferedRune = RuneType.None;

        _isValidatingInput = false;
        _isWaitingForRelease = false;

        _validationProgress = 0;
        _validationBreakerProgress = 0;
        _releaseProgress = 0;
    }

    public void ReadMessage(string message)
    {
        Process(message);
    }

    void Process(string message)
    {
        RuneType rune;
        switch (message)
        {
            default: rune = RuneType.None; break;
            case WATER: rune = RuneType.Water; break;
            case FIRE: rune = RuneType.Fire; break;
            case WOOD: rune = RuneType.Wood; break;
            case STONE: rune = RuneType.Stone; break;
        }


        if (_isValidatingInput)
        {
            if (rune == _bufferedRune)
            {
                _validationProgress++;
                _validationBreakerProgress = 0;
            }
            else
            {
                _validationBreakerProgress++;
                if (_validationProgress > 0) _validationProgress--;
            }
        }

        else if (_isWaitingForRelease && rune == RuneType.None)
            _releaseProgress++;

        //if it's waiting for a new rune
        else if (!_isValidatingInput && !_isWaitingForRelease && rune != RuneType.None)
        {
            _isValidatingInput = true;
            _bufferedRune = rune;
            _validationProgress = 1;
            _validationBreakerProgress = 0;
        }



        if (_isWaitingForRelease && _releaseProgress >= _releaseAmount)
        {
            _isWaitingForRelease = false;
            _bufferedRune = RuneType.None;
            OnRuneGone?.Invoke();
        }
        else if (_isValidatingInput)
        {
            if (_validationProgress >= _validationAmount)
            {
                _isValidatingInput = false;
                _isWaitingForRelease = true;
                _releaseProgress = 0;
                OnRuneValidated?.Invoke(rune);
            }
            else if (_validationBreakerProgress >= _validationBreakerAmount)
            {
                _isValidatingInput = false;
            }
        }
    }
}
