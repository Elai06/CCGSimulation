using DG.Tweening;
using UnityEngine;

public class GamingHandAnimation
{
    public void PositionAnimation(Transform cardTransform, Vector3 spawnPosition, Vector3 rotation)
    {
        cardTransform.DOLocalMove(spawnPosition, 0.5f);
        cardTransform.eulerAngles = rotation;
    }
}