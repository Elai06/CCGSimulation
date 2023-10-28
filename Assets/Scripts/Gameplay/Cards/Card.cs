using Gameplay.Animation;
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

        private NumberAnimation _numberAnimation = new();

        private ImageLoader _imageLoader;

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
    
        private int RandomNumber()
        {
            return Random.Range(-10, 10);
        }
    
        public void ChangeHealth()
        {
            _numberAnimation.Scale(_healthText, RandomNumber().ToString());
        }
    
        public void ChangeAttack()
        {
            _numberAnimation.Scale(_attackText, RandomNumber().ToString());
        }

    }
}