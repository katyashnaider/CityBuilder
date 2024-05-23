using System.Collections;
using TMPro;
using UnityEngine;

namespace CityBuilder
{
    public class TestViewCoinsUpdate : MonoBehaviour
    {
        [SerializeField] private TMP_Text _priceText;

        private void Start()
        {
            UpdatePrice(100);
            StartCoroutine(ChangePrice());
        }

        public void UpdatePrice(int price)
        {
            Debug.Log("TestViewCoinsUpdate UpdatePrice called with: " + price);
            _priceText.text = price.ToString();
            Debug.Log("TestViewCoinsUpdate Updated text: " + _priceText.text);
        }

        private IEnumerator ChangePrice()
        {
            yield return new WaitForSeconds(2);
            UpdatePrice(200);
        }
    }
}