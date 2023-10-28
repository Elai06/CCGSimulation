using Gameplay.Animation;
using UnityEngine;

namespace Gameplay.GamingHand
{
    public class BattlfieldPositionController : MonoBehaviour
    {
        private int _childCount;

        private void Start()
        {
            _childCount = transform.childCount;
        }

        private void Update()
        {
            if (_childCount != transform.childCount)
            {
                _childCount = transform.childCount;
                CenterCards();
            }
        }

        private void CenterCards()
        {
            for (int i = 0; i < _childCount; i++)
            {
                var card = transform.GetChild(i);
                var spawnPosition = Vector3.right * (i * 2);
                spawnPosition += Vector3.left * (_childCount - 1);
                GamingHandAnimation.MovePositionAnimation(card, spawnPosition, Vector3.zero);
            }
        }
    }
}