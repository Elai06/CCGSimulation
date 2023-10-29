using Gameplay.Animation;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.GamingHands
{
    public class BattlfieldPositionController : MonoBehaviour
    {
        [SerializeField] private Button _cancelButton;
        [SerializeField] private GamingHand _gamingHand;

        private int _childCount;

        private void Start()
        {
            _childCount = transform.childCount;
        }

        private void OnEnable()
        {
            _cancelButton.onClick.AddListener(Cancel);
        }

        private void OnDisable()
        {
            _cancelButton.onClick.RemoveListener(Cancel);
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

        private void Cancel()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                var card = transform.GetChild(i);
                card.transform.SetParent(_gamingHand.transform);
            }

            if (transform.childCount > 0)
            {
                Cancel();
            }
        }
    }
}