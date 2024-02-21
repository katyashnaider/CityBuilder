using TMPro;
using UnityEngine;

namespace CityBuilder.Workers.Wallet
{
    public class ViewWallet : MonoBehaviour
    {
        [SerializeField] private TMP_Text _wallet;

        public void UpdatePrice(int price)
        {
            _wallet.text = price.ToString();
        }
    }
}