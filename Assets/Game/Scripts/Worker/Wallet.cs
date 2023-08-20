using UnityEngine;

namespace Worker
{
    public class Wallet : MonoBehaviour
    {
        [SerializeField] private ViewWallet _viewWallet;
        
        private int _coins = 0;
        
        public bool HasEnoughCoins(int amount) => _coins >= amount;

        private void Start()
        {
            if (PlayerPrefs.HasKey("Wallet"))
                LoadProgress();

            _viewWallet.UpdatePrice(_coins);
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

        private void SaveProgress()
        {
            var progressHandler = new ProgressHandler();

            var saveData = new ProgressHandler.Save
            {
                Wallet = _coins
            };

            progressHandler.SaveProgress("Wallet", saveData);
        }

        private void LoadProgress()
        {
            var progressHandler = new ProgressHandler();
            var loadedData = progressHandler.LoadProgress("Wallet");

            _coins = loadedData.Wallet;
        }
    }
}