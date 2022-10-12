using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WSConnection))]
public class TestWS : MonoBehaviour
{
    WSConnection wsConnection;

    public const string WATER = "eau";
    public const string FIRE = "feu";
    public const string WOOD = "bois";
    public const string STONE = "pierre";

    RuneType _buffer;
    [Tooltip("Number of similar WS messages to confirm rune type"), SerializeField]
    int _detectionValidationAmount = 20;
    [Tooltip("Number of different consecutive WS messages to cancel verification"), SerializeField]
    int _validationBreakerAmount;

    int _validationProgress = 0;
    int _validationBreakerProgress = 0;

    private void Awake()
    {
        wsConnection = GetComponent<WSConnection>();
    }

    void Start()
    {
        wsConnection.OnMessageReceived.AddListener(OnMessage);
    }

    public void OnMessage(string message)
    {
        Process(message);
    }

    void Process(string message)
    {
        RuneType rune;
        switch (message)
        {
            default   : rune = RuneType.None; break;
            case WATER: rune = RuneType.Water; break;
            case FIRE : rune = RuneType.Fire; break;
            case WOOD : rune = RuneType.Wood; break;
            case STONE: rune = RuneType.Stone; break;
        }

        if(rune != RuneType.None)
        {
            Debug.Log(message);
        }
    }
}
