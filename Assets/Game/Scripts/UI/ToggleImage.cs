using UnityEngine;
using UnityEngine.UI;

namespace CityBuilder.UI
{
    public class ToggleImage : MonoBehaviour
    {
        [SerializeField] private Sprite[] _toggleSprites; // Массив спрайтов для переключения
        
        private int _currentIndex;
        private Image _imageComponent;

        private void Awake()
        {
            _imageComponent = GetComponent<Image>();
        }

        private void Start()
        {
            if (_toggleSprites.Length > 0)
            {
                _imageComponent.sprite = _toggleSprites[_currentIndex];
            }
        }

        public void Toggle()
        {
            if (_toggleSprites.Length == 0)
            {
                return;
            }

            _currentIndex = (_currentIndex + 1) % _toggleSprites.Length;
            _imageComponent.sprite = _toggleSprites[_currentIndex];
        }
    }
}