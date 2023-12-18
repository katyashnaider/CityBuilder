using CityBuilder.Save;
using CityBuilder.Worker;
using CityBuilder.Worker.Wallet;
using UnityEngine;

namespace CityBuilder.Upgrade
{
    public abstract class Upgrade : MonoBehaviour
    {
        [SerializeField] private UpgradeView _upgradeView;
        [SerializeField] protected Wallet _wallet;
        [SerializeField] protected int _upgradeAmount = 1;
        [SerializeField] protected int _price = 10;
        [SerializeField] private int _maxLevel = 5;
        
        private const int MultiplierPrice = 10;
        
        private int _counter;

        public int CurrentLevel { get; private set; }
        public int CurrentPrice { get; private set; }

        private void Awake()
        {
            CurrentPrice = _price;
            UpgradeInfo();
            UpdateButtonAvailability();
        }

        private void Update()
        {
            UpdateButtonAvailability();
        }

        public virtual void ApplyUpgrade()
        {
            if (!CanIncreaseLevel())
            {
                return;
            }

            IncreaseLevel();
            _wallet.SubtractCoins(CurrentPrice);
            IncreasePrice();
            UpgradeInfo();

            if (!CanIncreaseLevel())
            {
                CurrentPrice = int.MaxValue;
            }
        }

        public int GetValue()
        {
            return _upgradeAmount * CurrentLevel;
        }

        public void RestartLevel()
        {
            CurrentPrice = _price;
            CurrentLevel = 0;

            SaveProgress("AddWorkerUpgrade");
            SaveProgress("SpeedUpgrade");
            SaveProgress("IncomeUpgrade");
        }

        protected void UpgradeInfo()
        {
            if (CanIncreaseLevel())
            {
                _upgradeView.UpgradeDisplay(CurrentLevel, CurrentPrice);
            }
            else
            {
                _upgradeView.SetMaxLevel();
            }
        }

        protected void SaveProgress(string key)
        {
            ProgressHandler progressHandler = new ProgressHandler();

            ProgressHandler.Save saveData = new ProgressHandler.Save
            {
                LevelUpgrade = CurrentLevel,
                Price = CurrentPrice
            };

            progressHandler.SaveProgress(key, saveData);
        }

        protected void LoadProgress(string key)
        {
            ProgressHandler progressHandler = new ProgressHandler();
            ProgressHandler.Save loadedData = progressHandler.LoadProgress(key);

            CurrentLevel = loadedData.LevelUpgrade;
            CurrentPrice = loadedData.Price;
        }

        private bool CanIncreaseLevel()
        {
            return CurrentLevel < _maxLevel;
        }

        private void UpdateButtonAvailability()
        {
            if (_wallet.HasEnoughCoins(CurrentPrice))
            {
                _counter++;

                if (_counter == 1)
                {
                    _upgradeView.EnableOfButton();
                }
            }
            else
            {
                _counter = 0;
                _upgradeView.DisableOfButton();
            }
        }

        private void IncreaseLevel()
        {
            CurrentLevel++;
        }

        private void IncreasePrice()
        {
            CurrentPrice += CurrentPrice;
        }
    }
}