using System.Collections;
using Gameplay.Cards;
using Gameplay.GamingHands;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Gameplay
{
    public class GameplaySimulation : MonoBehaviour
    {
        [SerializeField] private Button _testButton;
        [SerializeField] private Button _battleButton;
        [SerializeField] private Button _restartButton;
        
        [SerializeField] private GamingHand _playerGamingHand;
        [SerializeField] private GamingHand _enemyGamingHand;
        
        [SerializeField] private BattlfieldMover _playerBattflield;
        [SerializeField] private BattlfieldMover _enemyBattflield;

        private void OnEnable()
        {
            _testButton.onClick.AddListener(Test);
            _restartButton.onClick.AddListener(Restart);
            _battleButton.onClick.AddListener(StartBattle);
        }

        private void OnDisable()
        {
            _testButton.onClick.RemoveListener(Test);
            _restartButton.onClick.RemoveListener(Restart);
            _battleButton.onClick.RemoveListener(StartBattle);
        }

        private void Test()
        {
            _playerGamingHand.SimulateDamage();
        }

        private void StartBattle()
        {
            StartCoroutine(Battle());
        }

        private IEnumerator Battle()
        {
            if (_playerBattflield.transform.childCount == 0 || _enemyBattflield.transform.childCount == 0)
            {
                yield break;
            }

            _playerGamingHand.BlockCardMover(true);
            _enemyGamingHand.BlockCardMover(true);

            var playerCard = _playerBattflield.GetCard(0);
            var enemyCard = _enemyBattflield.GetCard(_enemyBattflield.transform.childCount - 1);
            _playerGamingHand.UpdateCard(playerCard, playerCard.GetMover());
            _enemyGamingHand.UpdateCard(enemyCard, enemyCard.GetMover());

            yield return new WaitForSeconds(2);

            if (!playerCard.IsDied)
            {
                _playerBattflield.ReturnCardInHand(playerCard);
            }

            ReturnCard(_playerBattflield, playerCard);
            ReturnCard(_enemyBattflield, enemyCard);

            _playerGamingHand.BlockCardMover(false);
            _enemyGamingHand.BlockCardMover(false);

            StartCoroutine(Battle());
        }

        private void Restart()
        {
            SceneManager.LoadScene("Gameplay");
        }

        private void ReturnCard(BattlfieldMover battlfield, Card card)
        {
            if (!card.IsDied)
            {
                battlfield.ReturnCardInHand(card);
            }
        }
    }
}