using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GamingHand : MonoBehaviour
{
    [SerializeField] private int _maxCard = 8;
    [SerializeField] private Card _card;
    [SerializeField] private Transform _battlefieldTransform;

    private IList<Card> _cards = new List<Card>();

    private GamingHandAnimation _gamingHandAnimation = new();

    private int _childsCount;

    private bool _isInstatiateCardsEnd;

    private void Start()
    {
        StartCoroutine(InstatiateCards());
    }

    private void Update()
    {
        if (!_isInstatiateCardsEnd) return;
        if (_childsCount != gameObject.transform.childCount)
        {
            _childsCount = gameObject.transform.childCount;
            CorrectCardPosition();
        }
    }

    private void CorrectCardPosition()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            var isRightCard = i >= gameObject.transform.childCount - transform.childCount / 2;
            var card = transform.GetChild(i);
            SetPositionCard(isRightCard, card.transform, i, transform.childCount);
        }
    }

    private IEnumerator InstatiateCards()
    {
        for (var i = 0; i < _maxCard; i++)
        {
            yield return new WaitForSeconds(0.5f);
            var prefab = Instantiate(_card, Vector3.zero + (Vector3.up * 10 + Vector3.left * 5),
                Quaternion.identity, transform);
            var cardMover = prefab.gameObject.GetComponent<CardMover>();
            cardMover.SetParents(_battlefieldTransform, transform);

            var isRightCard = i >= _maxCard - _maxCard / 2;

            SetPositionCard(isRightCard, prefab.transform, i, _maxCard);
            _cards.Add(prefab);
        }

        _childsCount = gameObject.transform.childCount;
        _isInstatiateCardsEnd = true;
    }

    private void SetPositionCard(bool isRight, Transform cardTransform, int indexCard, int cardsCount)
    {
        var spawnPosition = Vector3.zero;
        var rotation = Vector3.zero;
        if (isRight)
        {
            var isRightIndex = indexCard - cardsCount / 2;
            isRightIndex = cardsCount % 2 != 0 ? isRightIndex : ++isRightIndex;

            spawnPosition += Vector3.right * 1.5f * isRightIndex;
            spawnPosition.y -= 0.2f * isRightIndex;
            rotation.z -= 5 * isRightIndex;
        }
        else
        {
            spawnPosition += Vector3.left * 1.5f * indexCard;
            spawnPosition.y -= 0.2f * indexCard;
            rotation.z += 5 * indexCard;
        }

        _gamingHandAnimation.PositionAnimation(cardTransform, spawnPosition, rotation);
    }
}