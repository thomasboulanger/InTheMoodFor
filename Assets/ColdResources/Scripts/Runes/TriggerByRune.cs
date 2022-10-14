using UnityEngine;
using UnityEngine.Events;

public class TriggerByRune : MonoBehaviour
{
    public UnityEvent OnWater, OnFire, OnWood, OnStone;

    public void Trigger(Rune rune)
        => Trigger(rune.Type);

    public void Trigger(RuneType type)
    {
        switch (type)
        {
            case RuneType.Water:
                OnWater?.Invoke();
                break;
            case RuneType.Fire:
                OnFire?.Invoke();
                break;
            case RuneType.Wood:
                OnWood?.Invoke();
                break;
            case RuneType.Stone:
                OnStone?.Invoke();
                break;
        }
    }
}
