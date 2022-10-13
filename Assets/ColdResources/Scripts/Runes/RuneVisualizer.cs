using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RuneHandler))]
public class RuneVisualizer : MonoBehaviour
{
    RuneHandler _runeHandler;
    [SerializeField] GameObject 
        _WaterRunePrefab,
        _FireRunePrefab,
        _WoodRunePrefab,
        _StoneRunePrefab;

    [SerializeField] GridLayoutGroup _gridLayout;
    Dictionary<Rune, GameObject> _runes;

    private void Awake()
    {
        _runeHandler = GetComponent<RuneHandler>();
    }

    private void Start()
    {
        _runes = new Dictionary<Rune, GameObject>();

        _runeHandler.OnRuneSpawned.AddListener(AddRune);
        _runeHandler.OnRuneDespawned.AddListener(RemoveRune);
        _runeHandler.OnRuneStartFading.AddListener(FadeRune);
    }

    GameObject GetPrefab(RuneType type)
    {
        switch (type)
        {
            default:
            case RuneType.Water: return _WaterRunePrefab;
            case RuneType.Fire: return _FireRunePrefab;
            case RuneType.Wood: return _WoodRunePrefab;
            case RuneType.Stone: return _StoneRunePrefab;
        }
    }

    private void AddRune(Rune rune)
    {
        _runes.Add(rune, Instantiate(GetPrefab(rune.Type), _gridLayout.transform));
    }

    private void RemoveRune(Rune rune)
    {
        if (_runes.ContainsKey(rune))
        {
            Destroy(_runes[rune]);
            _runes.Remove(rune);
        }
    }

    private void FadeRune(Rune rune)
    {
        if (_runes.ContainsKey(rune))
        {
            var image = _runes[rune].GetComponentInChildren<Image>();
            image.color = image.color * .5f;
        }
    }
}
