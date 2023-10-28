using DG.Tweening;
using UnityEngine;

public class CardAnimation
{
    private Tween _tween;

    public void ScaleAnimation(Transform transform, float scale)
    {
        _tween?.Kill();
        _tween = transform.DOScale(scale, 0.25f);
    }
}