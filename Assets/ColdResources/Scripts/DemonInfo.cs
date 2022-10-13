using UnityEngine;

public class DemonInfo : MonoBehaviour
{
    private RuneType _type;
    private Animator _animator;    
    private bool IsRuned;
    private bool _gameOver;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (IsRuned)
        {
            _animator.speed = 0;
        }
        else
        {
            _animator.speed = 1;
        }
        
    }

    public void HasFinishedAnimation()
    {
        
    }
}