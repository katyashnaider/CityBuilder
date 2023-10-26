using Scripts.Building;
using UnityEngine;

namespace Workers
{
    public class Wallet : MonoBehaviour
    {
        [SerializeField] private BuildingController _building;
        [SerializeField] private ViewWallet _viewWallet;

        private int _coins = 0;
        private int _bonus = 1000;

        public bool HasEnoughCoins(int amount) => _coins >= amount;

        private void Start()
        {
            if (PlayerPrefs.HasKey("Wallet"))
                _coins = LoadProgress();

            _viewWallet.UpdatePrice(_coins);
        }

        private void OnEnable() => 
            _building.ConstructedBuilding += OnConstructedBuilding;

        private void OnDisable() => 
            _building.ConstructedBuilding -= OnConstructedBuilding;

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

        private void OnConstructedBuilding()
        {
            _coins += _bonus;
            SaveProgress();
        }
        
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