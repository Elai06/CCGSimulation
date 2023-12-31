﻿using System.Collections.Generic;
using DG.Tweening;
using Gameplay.Animation;
using Gameplay.Enums;
using TMPro;
using UnityEngine;
using Utils;

namespace Gameplay.Cards
{
    public class Card : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _frontPicture;
        [SerializeField] private TextMeshPro _attackText;
        [SerializeField] private TextMeshPro _healthText;
        [SerializeField] private CardMover _cardMover;

        private Dictionary<ECardParametersType, int> _parametrs = new();

        private ImageLoader _imageLoader;

        public bool IsDied { get; private set; }

        private void Start()
        {
            SetRandomPicture();
        }

        private void SetRandomPicture()
        {
            _imageLoader = new ImageLoader();
            var texture = _imageLoader.LoadImage();

            if (texture == null)
            {
                return;
            }

            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
            _frontPicture.sprite = sprite;
        }

        private void ChangeParameter(ECardParametersType type, int value)
        {
            if (_parametrs.ContainsKey(type))
            {
                _parametrs[type] = value;
            }
        }
        
        public void ChangeHealth(int health)
        {
            NumberAnimation.Scale(_healthText, health.ToString());
            ChangeParameter(ECardParametersType.Health, health);
            
            IsDied = health <= 0;
        }

        public Sequence ChangeAttack(int attack)
        {
          var sequence = NumberAnimation.Scale(_attackText, attack.ToString());
            ChangeParameter(ECardParametersType.Attack, attack);

            return sequence;
        }

        public CardMover GetMover()
        {
            return _cardMover;
        }
    }
}