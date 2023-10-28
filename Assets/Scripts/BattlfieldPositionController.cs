using System;
using UnityEngine;

public class BattlfieldPositionController : MonoBehaviour
{
    private GamingHandAnimation _gamingHandAnimation = new();
    private int _childCount;

    private void Start()
    {
        _childCount = transform.childCount;
    }

    private void Update()
    {
        if (_childCount != transform.childCount)
        {
            CorrectCardPosition();
            _childCount = transform.childCount;
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

    private void SetPositionCard(bool isRight, Transform cardTransform, int indexCard, int cardsCount)
    {
        var spawnPosition = Vector3.zero;
        var rotation = Vector3.zero;
        if (isRight)
        {
            var isRightIndex = indexCard - cardsCount / 2;
            isRightIndex = cardsCount % 2 != 0 ? isRightIndex : ++isRightIndex;

            spawnPosition += Vector3.right * 2f * isRightIndex;
        }
        else
        {
            spawnPosition += Vector3.left * 2f * indexCard;
        }

        _gamingHandAnimation.PositionAnimation(cardTransform, spawnPosition, rotation);
    }
}