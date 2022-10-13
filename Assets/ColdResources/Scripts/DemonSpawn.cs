using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class DemonSpawn : MonoBehaviour
{
    public static List<GameObject> DemonList = new List<GameObject>();


    [SerializeField] private float _spawnDelay = 2f;
    [SerializeField] private List<int> _demonElementList = new List<int>();
    [SerializeField] private GameObject[] _demonPrefab;
    [SerializeField] private int _nbDemon = 8;

    private int _demonElementIndex;
    private int _demonCounter;
    private float _delayTimer;
    
    void Start()
    {
        DemonList.Add(gameObject);
    }

    void Update()
    {
        float deltaTime = Time.deltaTime;
        _delayTimer += deltaTime;
        if (_delayTimer >= _spawnDelay && _demonCounter < _nbDemon)
        {
            Debug.Log("spawn");
            _delayTimer = 0;
            SpawnDemon(_demonElementList[_demonElementIndex]);
            _demonElementIndex++;
            _demonCounter++;
        }
    }

    private void SpawnDemon(int index)
    {
        GameObject go = Instantiate(_demonPrefab[index], new Vector3(-10 + _demonCounter, 1, 0), quaternion.identity);
    }
}