using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;

public class UITweener : MonoBehaviour
{
    public bool HasInitialDelay = false;
    [ShowIf("HasInitialDelay")] public float InitialDelay = 0;
    [SerializeField, Min(float.Epsilon)] float _duration = 1;
    [SerializeField] bool _useCurrentScale;
    [SerializeField, HideIf("_useCurrentScale")] Vector2 _startScale;
    [SerializeField] Vector2 _targetScale;
    [SerializeField, EnumFlags] Axis _axis;
    [SerializeField] AnimationCurve _easeCurve;

    public void Tween()
    {
        Vector3 startScale = _startScale;
        startScale.z = transform.localScale.z;

        if (!_useCurrentScale) startScale = transform.localScale;

        Vector3 targetScale = new Vector3(
            ((_axis & Axis.Horizontal) == Axis.Horizontal) ? _targetScale.x : startScale.x,
            ((_axis & Axis.Vertical) == Axis.Vertical) ? _targetScale.y : startScale.y,
            startScale.z
            );

        var tween = transform.DOScale(targetScale, _duration).SetEase(_easeCurve);

        if (HasInitialDelay) tween.SetDelay(InitialDelay).onPlay += ()=> { transform.localScale = startScale; };
        else transform.localScale = startScale;
    }

    [System.Flags]
    enum Axis
    {
        None = 0,
        Vertical = 1 << 0,
        Horizontal = 1 << 1,
        Both = ~0
    };
}
