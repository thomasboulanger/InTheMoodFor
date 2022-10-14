using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenSequence : MonoBehaviour
{
    public List<UITweener> _sequence;

    public void Tween()
    {
        StartCoroutine(CrtSequence());

        IEnumerator CrtSequence()
        {
            foreach(var tweener in _sequence)
            {
                tweener.Tween();
                if(tweener.HasInitialDelay)
                    yield return new WaitForSeconds(tweener.InitialDelay);
            }
        }
    }
}
