using UnityEngine;
using DG.Tweening;

public class SealAnim : MonoBehaviour
{
    [SerializeField] AnimationCurve _spawnEaseCurve, _despawnEaseCurve;
    [SerializeField] RuneType _type;

    Tween _twFade, _twScaleOut;

    public void Spawn()
    {
        if (_twScaleOut != null && _twScaleOut.active) _twScaleOut.Kill();

        transform.localScale = Vector3.zero;
        gameObject.SetActive(true);
        transform.DOScale(Vector3.one, 1f).SetEase(_spawnEaseCurve);
    }

    public void Despawn()
    {
        if (_twFade != null && _twFade.active) _twFade.Kill();

        _twScaleOut = transform.DOScale(Vector3.zero, 1f).SetEase(_spawnEaseCurve);
        _twScaleOut.onComplete += ()=> gameObject.SetActive(false);
    }

    public void Fade(RuneType type)
    {
        if (type != _type) return;

        Debug.Log("Fade");
        _twFade = transform.DOShakeScale(10, .25f,4);
    }
}
