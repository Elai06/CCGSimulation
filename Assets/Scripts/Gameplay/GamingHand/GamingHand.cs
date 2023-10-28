using System.Collections.Generic;
using Gameplay.Cards;
using UnityEngine;

namespace Gameplay.GamingHand
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
    }
}