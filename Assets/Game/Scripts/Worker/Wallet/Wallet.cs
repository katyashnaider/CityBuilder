using CityBuilder.Save;
using UnityEngine;

namespace CityBuilder.Worker.Wallet
{
    public class Wallet : RestartEntity
    {
        [SerializeField] private ViewWallet _viewWallet;

        private const int Bonus = 1000;

        private int _coins;

        private void Start()
        {
            if (PlayerPrefs.HasKey("Wallet"))
            {
                _coins = LoadProgress();
            }

            _viewWallet.UpdatePrice(_coins);
        }

        public bool HasEnoughCoins(int amount)
        {
            return _coins >= amount;
        }

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
            _coins += Bonus;

            SaveProgress();
        }

        private void SaveProgress()
        {
            ProgressHandler progressHandler = new ProgressHandler();

            ProgressHandler.Save saveData = new ProgressHandler.Save
            {
                Wallet = _coins
            };

            progressHandler.SaveProgress("Wallet", saveData);
        }

        private int LoadProgress()
        {
            ProgressHandler progressHandler = new ProgressHandler();
            ProgressHandler.Save loadedData = progressHandler.LoadProgress("Wallet");

            return _coins = loadedData.Wallet;
        }
    }
}