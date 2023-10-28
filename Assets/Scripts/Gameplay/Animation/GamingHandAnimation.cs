using DG.Tweening;
using UnityEngine;

namespace Gameplay.Animation
{
    public class GamingHandAnimation
    {
        public static void MovePositionAnimation(Transform cardTransform, Vector3 spawnPosition, Vector3 rotation)
        {
            cardTransform.DOLocalMove(spawnPosition, 0.5f);
            cardTransform.DOLocalRotate(rotation, 0.5f);
        }
    }
}