using DG.Tweening;
using UnityEngine;

namespace Gameplay.Animation
{
    public class GamingHandAnimation
    {
        public static Tween MovePositionAnimation(Transform cardTransform, Vector3 spawnPosition, Vector3 rotation)
        {
            Tween tween;
            cardTransform.DOLocalMove(spawnPosition, 0.5f);
           tween = cardTransform.DOLocalRotate(rotation, 0.5f);

           return tween;
        }
    }
}