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
                _coins = PlayerPrefs.GetInt("Wallet", _coins);

            _viewWallet.UpdatePrice(_coins);
        }

        public void AddCoins(int price)
        {
            _coins += price;
            PlayerPrefs.SetInt("Wallet", _coins);
            _viewWallet.UpdatePrice(_coins);
        }

        public void SubtractCoins(int price)
        {
            _coins -= price;
            _viewWallet.UpdatePrice(_coins);
        }
    }
}