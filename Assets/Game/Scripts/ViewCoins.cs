using TMPro;
using UnityEngine;

public class ViewCoins : MonoBehaviour
{
    [SerializeField] private TMP_Text _price;

    public void UpdatePrice(int price) => _price.text = price.ToString();
}