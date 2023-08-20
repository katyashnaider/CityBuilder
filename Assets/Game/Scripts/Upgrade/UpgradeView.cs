using TMPro;
using UnityEngine;

namespace Upgrades
{
    internal class UpgradeView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _levelText;
        [SerializeField] private TMP_Text _priceText;

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
    }
}