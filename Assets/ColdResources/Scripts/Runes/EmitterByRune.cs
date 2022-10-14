using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using FMODUnity;
using FMOD.Studio;

public class EmitterByRune : MonoBehaviour
{
    [SerializeField] StudioEventEmitter Water, Fire, Wood, Stone;

    EventInstance instance;

    public void Play(Rune rune)
    {
        instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

        switch (rune.Type)
        {
            case RuneType.Water:
                instance = Water.EventInstance;
                break;
            case RuneType.Fire:
                instance = Fire.EventInstance;
                break;
            case RuneType.Wood:
                instance = Wood.EventInstance;
                break;
            case RuneType.Stone:
                instance = Stone.EventInstance;
                break;
        }
        instance.start();
    }

    public void Pause(bool state)
    {
        instance.setPaused(state);
    }


}
