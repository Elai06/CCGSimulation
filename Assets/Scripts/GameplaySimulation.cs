using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GameplaySimulation : MonoBehaviour
{
    [SerializeField] private Transform _gamingHand;
    [SerializeField] private Button _testButton;

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
        var randomCardIndex = Random.Range(0, _gamingHand.childCount);
        var card = _gamingHand.GetChild(randomCardIndex).GetComponent<Card>();
        card.ChangeAttack();
        card.ChangeHealth();

        card.transform.position += Vector3.back * 2;

        card.transform.DOScale(1.3f, 1f).SetLoops(2, LoopType.Yoyo).OnComplete(() =>
        {
            card.gameObject.transform.position += Vector3.forward * 2;
        });
    }
}