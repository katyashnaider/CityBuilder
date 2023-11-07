using System;
using Scripts;
using Scripts.Building;
using UnityEngine;

namespace Workers
{
    public class Wallet : RestartEntity
    {
        [SerializeField] private BuildingController _building;
        [SerializeField] private ViewWallet _viewWallet;

        private int _coins = 0;
        private readonly int _bonus = 1000;

        public bool HasEnoughCoins(int amount) => _coins >= amount;

        private void Start()
        {
            if (PlayerPrefs.HasKey("Wallet"))
                _coins = LoadProgress();

            _viewWallet.UpdatePrice(_coins);
        }

        // private void OnEnable() => 
        //     _building.ConstructedBuilding += OnConstructedBuilding;
        //
        // private void OnDisable() => 
        //     _building.ConstructedBuilding -= OnConstructedBuilding;

        public void AddCoins(int price)
        {
            _coins += price;
            SaveProgress();
            _viewWallet.UpdatePrice(_coins);
        }

        public void SubtractCoins(int price)
        {
            _coins -= price;
            SaveProgress();
            _viewWallet.UpdatePrice(_coins);
        }
        public override void Restart()
        {
            _coins = 0;
            _coins += _bonus;

            SaveProgress();
        }


        // private void OnConstructedBuilding()
        // {
        //     print("в OnConstructedBuilding до coins = " + _coins);
        //
        //     _coins += _bonus;
        //     SaveProgress();
        //     print("в OnConstructedBuilding после coins = " + _coins);
        //
        // }

        private void SaveProgress()
        {
            var progressHandler = new ProgressHandler();

            var saveData = new ProgressHandler.Save
            {
                Wallet = _coins
            };

            progressHandler.SaveProgress("Wallet", saveData);
        }

        private int LoadProgress()
        {
            var progressHandler = new ProgressHandler();
            var loadedData = progressHandler.LoadProgress("Wallet");

            return _coins = loadedData.Wallet;
        }
    }
}