using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Gameplay.Animation
{
    public class CardAnimation
    {
        private Tween _tween;

        public Tween ScaleAnimation(Transform transform, float scale)
        {
            _tween?.Kill();
            _tween = transform.DOScale(scale, 1f);

            return _tween;
        }
    }
}