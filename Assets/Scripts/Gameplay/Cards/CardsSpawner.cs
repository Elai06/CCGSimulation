using System.Collections;
using System.Collections.Generic;
using Gameplay.Animation;
using UnityEngine;

namespace Gameplay.Cards
{
    public class CardsSpawner : MonoBehaviour
    {
        [SerializeField] private Card _card;
        [SerializeField] private Transform _battlefieldTransform;
        [SerializeField] private int _maxCard = 8;

        public List<Card> InstatiateCards(Transform targetTransform)
        {
            var cards = new List<Card>();
            for (var i = 0; i < _maxCard; i++)
            {
                var prefab = Instantiate(_card, transform.position, Quaternion.identity, transform);
                cards.Add(prefab);

                var cardMover = prefab.gameObject.GetComponent<CardMover>();
                cardMover.SetDeckParents(_battlefieldTransform, targetTransform);
            }

            StartCoroutine(MoveCardsInHand(cards, targetTransform));
            return cards;
        }

        private IEnumerator MoveCardsInHand(List<Card> cards,Transform targetTransform)
        {
            foreach (var card in cards)
            {
                yield return new WaitForSeconds(0.5f);
                card.transform.SetParent(targetTransform);
            }
        }
    }
}