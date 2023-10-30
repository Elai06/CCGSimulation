using System.Collections.Generic;
using DG.Tweening;
using Gameplay.Animation;
using Gameplay.Cards;
using UnityEngine;

namespace Gameplay.GamingHands
{
    public class GamingHand : MonoBehaviour, IGamingHand
    {
        private const float WAIT_ANIMATION_FINISH = 1.5f;

        [SerializeField] private CardsSpawner _cardsSpawner;

        private IList<Card> _cards = new List<Card>();

        private void Start()
        {
            GetCards();
        }

        private void GetCards()
        {
            _cards = _cardsSpawner.InstatiateCards(transform);
        }

        public void UpdateCard(Card card, CardMover cardMover)
        {
            cardMover.ShowCard();

            card.ChangeHealth(RandomNumber());
            card.ChangeAttack(RandomNumber());

            HideCard(card, cardMover);
        }

        public void SimulateDamage()
        {
            if (!IsHaveCardsInHand()) return;

            var card = GetRandomCard();
            var cardMover = card.GetComponent<CardMover>();

            UpdateCard(card, cardMover);
        }

        private void HideCard(Card card, CardMover cardMover)
        {
            DOVirtual.DelayedCall(WAIT_ANIMATION_FINISH, () =>
            {
                if (card.IsDied)
                {
                    RemoveCard(card);
                }
                else
                {
                    cardMover.HideCard();
                }
            });
        }

        private Card GetRandomCard()
        {
            var card = _cards[Random.Range(0, _cards.Count)];

            if (!IsCardIsHand(card))
            {
                return GetRandomCard();
            }

            return card;
        }

        public void RemoveCard(Card card)
        {
            var position = Vector3.right * 25 + Vector3.up * 5;
            card.transform.SetParent(card.transform.parent.parent);
            _cards.Remove(card);
            GamingHandAnimation.MovePositionAnimation(card.transform, position, Vector3.zero);
            DOVirtual.DelayedCall(1, () => Destroy(card.gameObject));
        }

        private int RandomNumber()
        {
            return Random.Range(-10, 10);
        }

        private bool IsCardIsHand(Card card)
        {
            return card.transform.parent == transform;
        }

        private bool IsHaveCardsInHand()
        {
            return transform.childCount > 0;
        }

        public void BlockCardMover(bool isBlock)
        {
            for (int i = 0; i < _cards.Count; i++)
            {
                var card = _cards[i];
                card.GetMover().SetBlock(isBlock);
            }
        }
    }
}