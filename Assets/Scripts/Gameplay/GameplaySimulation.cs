using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{
    public class GameplaySimulation : MonoBehaviour
    {
        [SerializeField] private Button _testButton;
        [SerializeField] private GamingHands.GamingHand _gamingHand;


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
            _gamingHand.UpdateCharacteristics();
        }
    }
}