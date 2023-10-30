using Gameplay.Animation;
using UnityEngine;

namespace Gameplay.GamingHands
{
    public class GamingHandMover : MonoBehaviour
    {
        private const int ROTATION_OFFSET = 5;
        private const float HEIGHT_STEP = 0.15f;

        private int _childsCount;
        private bool _isInstatiateCardsEnd;

        private void Update()
        {
            if ( _childsCount == gameObject.transform.childCount) return;
            _childsCount = gameObject.transform.childCount;
            CenterCards();
        }

        private void CenterCards()
        {
            var rotation = Vector3.zero;
            rotation.z = (_childsCount + 1) * ROTATION_OFFSET / 2f;
            var height = _childsCount / 2f * HEIGHT_STEP;

            for (int i = 0; i < _childsCount; i++)
            {
                var card = transform.GetChild(i);
                var spawnPosition = Vector3.right * i;
                spawnPosition += Vector3.left * (_childsCount - 1) / 2;

                height = GetHeightForCard(i, height);

                spawnPosition.y -= height;

                rotation.z -= ROTATION_OFFSET;
                GamingHandAnimation.MovePositionAnimation(card, spawnPosition, rotation);
            }
        }

        private float GetHeightForCard(int i, float height)
        {
            if (i == 0)
            {
                return height;
            }

            if (i > _childsCount / 2f)
            {
                height += HEIGHT_STEP;
            }
            else
            {
                height -= HEIGHT_STEP;
            }

            return height;
        }
    }
}