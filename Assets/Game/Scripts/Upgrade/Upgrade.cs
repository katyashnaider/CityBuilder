﻿using System;
using UnityEngine;
using Workers;

namespace Upgrades
{
    public abstract class Upgrade : MonoBehaviour
    {
        [SerializeField] private UpgradeView _upgradeView;
        [SerializeField] protected Wallet _wallet;
        [SerializeField] protected int _upgradeAmount = 1;
        [SerializeField] protected int _price = 10;
        [SerializeField] private int _multiplierPrice = 2;

        private int _currentPrice = 0;
        private int _currentLevel = 0;
        private int _counter = 0;

        private const int MaxLevel = 5;

        public int CurrentLevel => _currentLevel;
        public int CurrentPrice => _currentPrice;

        private void Awake()
        {
            _currentPrice = _price;
            UpgradeInfo();
            UpdateButtonAvailability();
        }

        private void Update() =>
            UpdateButtonAvailability();

        public virtual void ApplyUpgrade()
        {
            if (!CanIncreaseLevel())
                return;

            IncreaseLevel();
            _wallet.SubtractCoins(_currentPrice);
            IncreasePrice();
            UpgradeInfo();
        }

        public int GetValue() => 
            _upgradeAmount * _currentLevel;

        public void RestartLevel()
        {
            _currentPrice = _price;
            _currentLevel = 0;
            
           // _upgradeView.DisableOfButton();
            _upgradeView.UpgradeDisplay(_currentLevel, _currentPrice);
        }
        
        protected void UpgradeInfo()
        {
            if (CanIncreaseLevel())
                _upgradeView.UpgradeDisplay(_currentLevel, _currentPrice);
            else
                _upgradeView.SetMaxLevel();
        }

        protected void SaveProgress(string key)
        {
            var progressHandler = new ProgressHandler();

            var saveData = new ProgressHandler.Save
            {
                LevelUpgrade = _currentLevel,
                Price = _currentPrice
            };

            progressHandler.SaveProgress(key, saveData);
        }

        protected void LoadProgress(string key)
        {
            var progressHandler = new ProgressHandler();
            var loadedData = progressHandler.LoadProgress(key);

            _currentLevel = loadedData.LevelUpgrade;
            _currentPrice = loadedData.Price;
        }

        private bool CanIncreaseLevel() => _currentLevel < MaxLevel;

        private void UpdateButtonAvailability()
        {
            if (_wallet.HasEnoughCoins(CurrentPrice))
            {
                _counter++;

                if (_counter == 1)
                    _upgradeView.EnableOfButton();
            }
            else
            {
                _counter = 0;
                _upgradeView.DisableOfButton();
            }
        }

        private void IncreaseLevel() => _currentLevel++;

        private void IncreasePrice() => _currentPrice += _multiplierPrice;

    }
}