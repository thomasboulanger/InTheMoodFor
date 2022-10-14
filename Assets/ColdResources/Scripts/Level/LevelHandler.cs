using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class LevelHandler : MonoBehaviour
{
    [SerializeField] DemonSpawn _spawner;
    [SerializeField] LoadLevel _reloader, _nextLevelLoader;
    public UnityEvent OnVictory, OnDefeat;

    bool _defeat = false, _victory = false;

    void Start()
    {
        _spawner.OnLevelFinish.AddListener(TryVictory);
        DemonInfo.OnDemonRaised += TryDefeat;
    }

    void OnDestroy()
    {
        _spawner?.OnLevelFinish.RemoveListener(TryVictory);
        DemonInfo.OnDemonRaised -= TryDefeat;
    }

    private void TryVictory()
    {
        if (_victory || _defeat) return;
        _victory = true;

        StartCoroutine(CrtVictory());

        IEnumerator CrtVictory()
        {
            //Handle Victory
            OnVictory?.Invoke();
            //-->
            //--> disable running systems
            //--> play victory anim
            //----> trigger audio morning
            //----> kill/hide demons
            yield return new WaitForSeconds(5f);
            //--> load next level
            _nextLevelLoader.Load();
            //-->
        }
    }

    private void TryDefeat()
    {
        if (_victory || _defeat) return;
        _defeat = true;

        StartCoroutine(CrtDefeat());

        IEnumerator CrtDefeat()
        {
            //Handle Defeat
            OnDefeat?.Invoke();
            //-->
            //--> disable running systems
            //--> play defeat anim
            yield return new WaitForSeconds(5f);
            //--> reload level
            _reloader.Load();
            //-->
        }
    }
}
