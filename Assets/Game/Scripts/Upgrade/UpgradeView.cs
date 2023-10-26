using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Upgrades
{
    internal class UpgradeView : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _levelText;
        [SerializeField] private TMP_Text _priceText;
        [SerializeField] private Image _image;
        [SerializeField] private Image _imageIconCoin;
        [SerializeField] private Sprite _enabledIcon;
        [SerializeField] private Sprite _enabledIconCoin;
        [SerializeField] private Sprite _disabledIcon;
        [SerializeField] private Sprite _disabledIconCoin;

        public void UpgradeDisplay(int currentLevel, int currentPrice)
        {
            _levelText.text = currentLevel.ToString();
            _priceText.text = currentPrice.ToString();
        }

        public void SetMaxLevel()
        {
            _levelText.text = "MAX";
            _priceText.text = "—";
        }

        public void EnableOfButton()
        {
            _button.animator.SetTrigger("Enabled");
            
            _image.sprite = _enabledIcon;
            _imageIconCoin.sprite = _enabledIconCoin;
            _button.interactable = true;
        }
        
        public void DisableOfButton()
        {
            _image.sprite = _disabledIcon;
            _imageIconCoin.sprite = _disabledIconCoin;
            _button.interactable = false;
        }
    }
}