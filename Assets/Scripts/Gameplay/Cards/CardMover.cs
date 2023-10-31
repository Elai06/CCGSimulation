using System;
using System.Threading.Tasks;
using DG.Tweening;
using Gameplay.Animation;
using UnityEngine;

namespace Gameplay.Cards
{
    public class CardMover : MonoBehaviour, ICardMover
    {
        [SerializeField] private Vector3 _hoverScale = new(1.5f, 1.5f, 1.5f);

        private CardAnimation _cardAnimation = new();

        private bool _isDragging;

        private Transform _previusParent;
        private Transform _gameHandTransform;
        private Transform _battlefieldTransform;

        private Vector3 _originalScale;
        private Vector3 _offset;

        private bool _isBlock;

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
            if (_isBlock) return;

            ShowCard();
        }

        private void OnMouseExit()
        {
            if (_isBlock) return;

            HideCard();
        }

        public Tween ShowCard()
        {
            gameObject.transform.position += Vector3.back * 2;
           return _cardAnimation.ScaleAnimation(transform, _hoverScale.x);
        }

        public void HideCard()
        {
            _cardAnimation.ScaleAnimation(transform, _originalScale.x);
            gameObject.transform.position += Vector3.forward * 2;
        }

        private void OnMouseDown()
        {
            if (_isBlock) return;

            _isDragging = true;
            _offset = gameObject.transform.position - GetMouseWorldPosition();
            _offset.z = 0;
            transform.SetParent(transform.parent.parent);
        }

        private void OnMouseUp()
        {
            if (_isBlock) return;

            _isDragging = false;
            ChangeDeck();
        }

        private void ChangeDeck()
        {
            if (_battlefieldTransform.localPosition.y - transform.localPosition.y <= 3)
            {
                gameObject.transform.SetParent(_battlefieldTransform);
                return;
            }

            if (transform.localPosition.y - _gameHandTransform.localPosition.y <= 3)
            {
                gameObject.transform.SetParent(_gameHandTransform);
                return;
            }

            gameObject.transform.SetParent(_previusParent);
        }

        private void Update()
        {
            if (_isDragging && !_isBlock)
            {
                var mousePosition = GetMouseWorldPosition();
                mousePosition.z = -2;
                gameObject.transform.position = mousePosition + _offset;
            }
        }

        private Vector3 GetMouseWorldPosition()
        {
            Vector3 mousePoint = Input.mousePosition;
            mousePoint.z = 0;
            return Camera.main.ScreenToWorldPoint(mousePoint);
        }

        public void SetBlock(bool isBlock)
        {
            _isBlock = isBlock;
        }
    }
}