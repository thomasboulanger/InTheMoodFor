using UnityEngine;

public class DemonInfo : MonoBehaviour
{
    [field: SerializeField] public RuneType Type { get; private set; }
    private Animator _animator;    
    private bool IsRuned;
    private bool _gameOver;

    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        
    }

    public void Ward()
    {
        _animator.speed = 0;
    }

    public void Unward()
    {
        _animator.speed = 1;
    }

    public void HasFinishedAnimation()
    {
        
    }
}