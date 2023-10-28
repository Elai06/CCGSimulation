using DG.Tweening;
using Gameplay.GamingHand;
using UnityEngine;
using UnityEngine.UI;
using Gameplay.Cards;

namespace Gameplay
{
    public class GameplaySimulation : MonoBehaviour
    {
        [SerializeField] private Button _testButton;
        [SerializeField] private GamingHandDeckMover _gamingHandDeckMover;
        

        private void OnEnable()
        {
            _testButton.onClick.AddListener(Test);
        }

        private void OnDisable()
        {
            _testButton.onClick.RemoveListener(Test);
        }

        private void Test()
        {
            var gamingHandTransform = _gamingHandDeckMover.transform;
            if (gamingHandTransform.childCount == 0)
            {
                Debug.Log($"Don't have child {gamingHandTransform.childCount}");
                return;
            }

            var randomCardIndex = Random.Range(0, gamingHandTransform.childCount);
            var card = gamingHandTransform.GetChild(randomCardIndex).GetComponent<Card>();
            card.ChangeAttack();
            card.ChangeHealth();
            card.transform.position += Vector3.back * 2;
            card.transform.DOScale(1.3f, 1f).SetLoops(2, LoopType.Yoyo).OnComplete(() =>
            {
                card.gameObject.transform.position += Vector3.forward * 2;
            });
        }
    }
}