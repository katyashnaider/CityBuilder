using UnityEngine;
using UnityEngine.UI;

namespace CityBuilder.UI
{
    public class ToggleImage : MonoBehaviour
    {
        [SerializeField] private Sprite[] _toggleSprites;
        
        private int _currentIndex;
        private Image _imageComponent;

        private void Awake()
        {
            _imageComponent = GetComponent<Image>();

            _currentIndex = PlayerPrefs.HasKey("SoundIcon") ? PlayerPrefs.GetInt("SoundIcon", _currentIndex) : 0;
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
            _currentIndex = PlayerPrefs.HasKey("SoundIcon") ? PlayerPrefs.GetInt("SoundIcon", _currentIndex) : 0;
            
            if (_toggleSprites.Length == 0)
            {
                return;
            }

            _currentIndex = (_currentIndex + 1) % _toggleSprites.Length;
            _imageComponent.sprite = _toggleSprites[_currentIndex];

            SaveIcon();
        }

        private void SaveIcon()
        {
            PlayerPrefs.SetInt("SoundIcon", _currentIndex);
            PlayerPrefs.Save();
        }
    }
}