using DG.Tweening;
using TMPro;

namespace Gameplay.Animation
{
    public class NumberAnimation
    {
        public static void Scale(TextMeshPro text, string number)
        {
            var sequence = DOTween.Sequence();
            sequence.Append(text.rectTransform.DOScale(1.5f, 1f).OnComplete(() => { text.text = number; })
                    .SetEase(Ease.OutQuint))
                .Append(text.rectTransform.DOScale(1, 1f).SetEase(Ease.InQuint));
        }
    }
}