using System;
using UnityEngine;

public class DemonInfo : MonoBehaviour
{
    public static event Action OnDemonRaised;

    [field: SerializeField] public RuneType Type { get; private set; }
    private Animator _animator;    
    private bool IsWarded;


    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
    }

    public void Ward()
    {
        IsWarded = true;
        _animator.speed = 0;
    }

    public void Unward()
    {
        IsWarded = false;
        _animator.speed = 1;
    }

    public void HasFinishedAnimation()
    {
        OnDemonRaised?.Invoke();
    }
}