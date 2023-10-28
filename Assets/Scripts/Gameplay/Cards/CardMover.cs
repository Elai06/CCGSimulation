using Gameplay.Animation;
using UnityEngine;

namespace Gameplay.Cards
{
    public class CardMover : MonoBehaviour, ICardMover
    {
        [SerializeField] private Vector3 _hoverScale = new(1.5f, 1.5f, 1.5f);

        private Transform _gameHandTransform;
        private Transform _battlefieldTransform;

        private CardAnimation _cardAnimation = new();

        private bool _isDragging;

        private Vector3 _originalScale;
        private Vector3 _offset;

        private Transform _previusParent;

        private void Start()
        {
            _originalScale = transform.localScale;
            _previusParent = gameObject.transform.parent;
        }

        public void SetDeckParents(Transform battlefield, Transform gameplayHand)
        {
            _battlefieldTransform = battlefield;
            _gameHandTransform = gameplayHand;
        }

        private void OnMouseEnter()
        {
            ShowCard();
        }

        private void OnMouseExit()
        {
            ReturnCard();
        }
    
        public void ShowCard()
        {
            _cardAnimation.ScaleAnimation(transform, _hoverScale.x);
            gameObject.transform.position += Vector3.back * 2;
        }

        public void ReturnCard()
        {
            _cardAnimation.ScaleAnimation(transform, _originalScale.x);
            gameObject.transform.position += Vector3.forward * 2;
        }

        private void OnMouseDown()
        {
            _isDragging = true;
            _offset = gameObject.transform.position - GetMouseWorldPosition();
            _offset.z = 0;
            transform.SetParent(transform.root.parent);
        }

        private void OnMouseUp()
        {
            _isDragging = false;

            ChangeDeck();
        }
    
        private void ChangeDeck()
        {
            if (_battlefieldTransform.position.y - transform.position.y <= 3)
            {
                gameObject.transform.SetParent(_battlefieldTransform);
                return;
            }

            if (transform.position.y - _gameHandTransform.position.y <= 3)
            {
                gameObject.transform.SetParent(_gameHandTransform);
                return;
            }
        
            gameObject.transform.SetParent(_previusParent);
        }

        private void Update()
        {
            if (_isDragging)
            {
                var mousePosition = GetMouseWorldPosition();
                mousePosition.z = -2;
                gameObject.transform.position = mousePosition + _offset;
            }
        }

        private Vector3 GetMouseWorldPosition()
        {
            Vector3 mousePoint = Input.mousePosition;
            mousePoint.z = 9;
            return Camera.main.ScreenToWorldPoint(mousePoint);
        }
    }
}