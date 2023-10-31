using System.Collections.Generic;
using DG.Tweening;
using Gameplay.Animation;
using Gameplay.Cards;
using UnityEngine;

namespace Gameplay.GamingHands
{
    public class GamingHand : MonoBehaviour, IGamingHand
    {
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

        public void SimulateDamage()
        {
            if (!IsHaveCardsInHand()) return;

            var card = GetRandomCard();
            var cardMover = card.GetComponent<CardMover>();

            UpdateCard(card, cardMover);
        }

        public Sequence UpdateCard(Card card, CardMover cardMover)
        {
            cardMover.ShowCard();

            card.ChangeHealth(RandomNumber());
            var seequnce = card.ChangeAttack(RandomNumber());

            seequnce?.OnComplete(() => { HideCard(card, cardMover); });
            return seequnce;
        }
        
        private void HideCard(Card card, CardMover cardMover)
        {
            if (card.IsDied)
            {
                RemoveCard(card);
            }
            else
            {
                cardMover.HideCard();
            }
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
            var tween = GamingHandAnimation.MovePositionAnimation(card.transform, position, Vector3.zero);
            tween?.OnComplete(() => { Destroy(card.gameObject); });
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