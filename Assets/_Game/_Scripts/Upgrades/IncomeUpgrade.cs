using System;
using UnityEngine;

namespace CityBuilder.Upgrades
{
    public class IncomeUpgrade : Upgrade
    {
        public event Action<int> ChangedIncome;
        
        private void Start()
        {
            if (PlayerPrefs.HasKey("IncomeUpgrade"))
            {
                LoadProgress("IncomeUpgrade");
            }

            UpgradeInfo();
        }

        public override void ApplyUpgrade()
        {
            if (_wallet.HasEnoughCoins(CurrentPrice))
            {
                base.ApplyUpgrade();
                SaveProgress("IncomeUpgrade");
                ChangedIncome?.Invoke(_upgradeAmount);
            }
        }
    }
}