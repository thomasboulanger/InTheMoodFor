using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class DemonHandler : MonoBehaviour
{
    List<DemonInfo> _demons;
    public List<SpawnSlot> SpawnSlots { get; private set; }
    private List<SpawnSlot> _usableSlots;
    private List<SpawnSlot> _usedSlots;

    public UnityEvent<Rune> OnRuneAdded;

    void Awake()
    {
        SpawnSlots = GameObject.FindGameObjectsWithTag("SpawnPoint").Select(tf => tf.GetComponent<SpawnSlot>()).ToList();
        _usableSlots = new List<SpawnSlot>(SpawnSlots);
        _usedSlots = new List<SpawnSlot>();
    }

    public void AddDemon(DemonInfo enemy)
    {
        SpawnSlot slot = _usableSlots[Random.Range(0, _usableSlots.Count)];

        enemy.transform.position = slot.transform.position;
        enemy.transform.LookAt(Vector3.zero);

        slot.AddEnemy(enemy);
        _usableSlots.Remove(slot);
        _usedSlots.Add(slot);

    }

    public bool TryAddRune(Rune rune)
    {
        var chosenSlot = _usedSlots
            .Where(slot => slot.Enemy.Type == rune.Type) // only slots where the enemy is of the same type
            .Where(slot => slot.Rune == null || !slot.Rune.IsActive || slot.Rune.IsFading) // only slots protected by no rune or a fading one
            .OrderByDescending(slot => slot.Rune == null || !slot.Rune.IsActive) // priorize unprotected slots
            .FirstOrDefault();

        if (chosenSlot == null) return false;
        else
        {
            chosenSlot.AddRune(rune);
            OnRuneAdded?.Invoke(rune);
            return true;
        }
    }

    void Update()
    {
        foreach(var slot in _usedSlots)
        {
            if(slot.Rune != null && !slot.Rune.IsActive)
            {
                slot.RemoveRune();
            }
        }
    }
}
