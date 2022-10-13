using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class DemonSpawn : MonoBehaviour
{
    public static List<GameObject> DemonList = new List<GameObject>();

    [SerializeField] private LevelConfig _levelConfig;
    [SerializeField] private GameObject[] _demonPrefab;
    [SerializeField] private float _demonAnimationTime;
    
    private List<GameObject> _spawnPoints = new List<GameObject>();
    private List<GameObject> _usableSpawnPoints;
    private int _demonElementIndex;
    private int _demonCounter;
    private float _startTime;
    private int _dayCounter;


    private void Start()
    {
        _startTime = Time.time;
        foreach (GameObject element in GameObject.FindGameObjectsWithTag("SpawnPoint"))
        {
            _spawnPoints.Add(element);
        }
        _usableSpawnPoints = _spawnPoints;
    }

    private void Update()
    {
        float currentTime = Time.time - _startTime;
        if (_demonCounter < _levelConfig.DemonSpawnTimer.Count && currentTime >= _levelConfig.DemonSpawnTimer[_demonCounter])
        {
            SpawnDemon(_levelConfig.DemonOrderOfElement[_demonCounter]);
            _demonCounter++;
        }
    }

    private void SpawnDemon(int index)
    {
        int rnd = Random.Range(0, _usableSpawnPoints.Count);
        GameObject go = Instantiate(_demonPrefab[index], _usableSpawnPoints[rnd].transform.position, quaternion.identity);
        _usableSpawnPoints.RemoveAt(rnd);
        DemonList.Add(go); 
        go.GetComponent<DemonInfo>().Init(_demonAnimationTime);
    }
}