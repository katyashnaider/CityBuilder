using UnityEngine;
using Worker;

namespace Upgrades
{
    public abstract class Upgrade : MonoBehaviour
    {
        [SerializeField] private UpgradeView _upgradeView;
        [SerializeField] protected Wallet _wallet;
        [SerializeField] protected int _upgradeAmount = 1;
        [SerializeField] protected int _price = 10;
        [SerializeField] private int _multiplierPrice = 2;

        protected int CurrentPrice = 0;

        private int _currentLevel = 1;

        private const int MaxLevel = 5;

        private void Start()
        {
            CurrentPrice = _price;
            UpgradeInfo();
        }

        public virtual void ApplyUpgrade()
        {
            if (!CanIncreaseLevel())
                return;

            IncreaseLevel();
            _wallet.SubtractCoins(CurrentPrice);
            IncreasePrice();
            SaveProgress();
            UpgradeInfo();
        }

        public void UpgradeInfo()
        {
            if (CanIncreaseLevel())
                _upgradeView.UpgradeDisplay(_currentLevel, CurrentPrice);
            else
                _upgradeView.SetMaxLevel();
        }

        protected void LoadProgress()
        {
            var json = PlayerPrefs.GetString("01");
            var upgradeSave = JsonUtility.FromJson<Save>(json);

            _currentLevel = upgradeSave.CurrentLevel;
            CurrentPrice = upgradeSave.CurrentPrice;
        }

        protected bool CanIncreaseLevel() => _currentLevel < MaxLevel;

        private void IncreaseLevel() => _currentLevel++;

        private void IncreasePrice() => CurrentPrice += _multiplierPrice;

        private void SaveProgress()
        {
            var upgradeSave = new Save();

            upgradeSave.CurrentLevel = _currentLevel;
            upgradeSave.CurrentPrice = CurrentPrice;

            var json = JsonUtility.ToJson(upgradeSave);

            PlayerPrefs.SetString("01", json);
        }

    }
}