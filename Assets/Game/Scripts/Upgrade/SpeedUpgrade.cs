using System;
using UnityEngine;

namespace Upgrades
{
    public class SpeedUpgrade : Upgrade
    {
        // [SerializeField] private TMP_Text _levelText;
        //  [SerializeField] private TMP_Text _priceText;
        //[SerializeField] private Wallet _wallet;
        //[SerializeField] private float _upgradeAmount = 1f;
        //[SerializeField] private int _price = 10;

        //private int _currentLevel = 1;
        //private int _currentPrice = 0;
        //private int _multiplier = 2;

        //private const int MaxLevel = 5;

        public event Action<float> ChangedSpeed;

        private void Start()
        {
            if (PlayerPrefs.HasKey("SpeedUpgrade"))
                LoadProgress("SpeedUpgrade");
            else
                CurrentPrice = _price;

            UpgradeInfo();

            //получаем из PlayerPrefs текущий уровень улучшения
            //метод обновляет текущую стоимость улучшения (обновить текст на кнопке) и установить новое значение цены
            //проходимся через цикл и вызываем IncreasePrice = уровню - 1
            //метод где если уровень максимальный, то показал что уровень MAX
        }

        public override void ApplyUpgrade()
        {
            if (_wallet.HasEnoughCoins(CurrentPrice))
            {
                base.ApplyUpgrade();
                SaveProgress("SpeedUpgrade");
                ChangedSpeed?.Invoke(_upgradeAmount);
            }
        }

        //public void ApplyUpgrade()
        //{
        //    if (!CanIncreaseLevel())
        //        return;

        //    if (_wallet.HasEnoughCoins(_currentPrice))
        //    {
        //        IncreaseLevel();
        //        _wallet.SubtractCoins(_currentPrice);
        //        IncreasePrice();
        //        ChangedSpeed?.Invoke(_upgradeAmount);

        //        SaveProgress();
        //        UpdateUpgradeInfoUI();

        //        //if (!CanIncreaseLevel())
        //        //SetMaxLevel();
        //    }
        //}

        //private void SaveProgress()
        //{
        //    var upgrade = new UpgradeSave();

        //    upgrade.CurrentLevel = _currentLevel;
        //    upgrade.CurrentPrice = _currentPrice;

        //    var json = JsonUtility.ToJson(upgrade);

        //    PlayerPrefs.SetString("01", json);
        //}

        //private void UpdateUpgradeInfoUI()
        //{
        //    if (CanIncreaseLevel())
        //    {
        //        _levelText.text = _currentLevel.ToString();
        //        _priceText.text = CurrentPrice.ToString();
        //    }
        //    else
        //    {
        //        SetMaxLevel();
        //    }
        //}

        //private void LoadProgress()
        //{
        //    var json = PlayerPrefs.GetString("01");
        //    var ugrade = JsonUtility.FromJson<UpgradeSave>(json);

        //    _currentLevel = ugrade.CurrentLevel;
        //    _currentPrice = ugrade.CurrentPrice;

        //    print(ugrade.CurrentLevel);
        //    print(ugrade.CurrentPrice);

        //}

        //private void SetMaxLevel()
        //{
        //    _levelText.text = "MAX";
        //    _priceText.text = "—";
        //}

        //private bool CanIncreaseLevel() => _currentLevel < MaxLevel;

        //private void IncreaseLevel() => _currentLevel++;

        //private void IncreasePrice() => _currentPrice += _multiplier;

        //[Serializable]
        //public struct UpgradeSave
        //{
        //    public int CurrentLevel;
        //    public int CurrentPrice;
        //}
    }
}