using UnityEngine;

public class DemonInfo : MonoBehaviour
{
    private bool IsRuned;
    private float _timerToLose;

    private void Update()
    {
        if (IsRuned)
        {
            transform.GetComponent<Animation>(); // pause animation
        }
        else
        {
            transform.GetComponent<Animation>(); // jouer l'animation
            _timerToLose -= Time.deltaTime;
        }

        if (_timerToLose <= 0) GameOver();
    }

    public void Init(float timeBeforeLose)
    {
        _timerToLose = timeBeforeLose;
    }

    private void GameOver()
    {
    }
}