using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class DemonSpawn : MonoBehaviour
{
    public UnityEvent OnStartSpawning, OnLevelFinish;
    bool _levelRunning = false;

    [SerializeField] private LevelConfig _levelConfig;
    [SerializeField] private GameObject[] _demonPrefab;
    [SerializeField] DemonHandler _demonHandler;
    
    private int _demonCounter;
    private float _startTime;

    private void Start()
    {
        _startTime = Time.time;
        _levelRunning = true;
        OnStartSpawning?.Invoke();
    }

    private void Update()
    {
        float currentTime = Time.time - _startTime;
        if (_demonCounter < _levelConfig.DemonSpawnTimer.Count && currentTime >= _levelConfig.DemonSpawnTimer[_demonCounter])
        {
            SpawnDemon(_levelConfig.DemonOrderOfElement[_demonCounter]);
            _demonCounter++;
        }

        if (_levelRunning && currentTime >= _levelConfig.LevelDuration)
        {
            _levelRunning = false;
            OnLevelFinish?.Invoke();
        }
    }

    private void SpawnDemon(int index)
    {
        GameObject demon = Instantiate(_demonPrefab[index], Vector3.zero, quaternion.identity);
        _demonHandler.AddDemon(demon.GetComponent<DemonInfo>());
    }
}