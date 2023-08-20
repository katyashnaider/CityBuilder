using System;
using UnityEngine;

namespace Upgrades
{
    public class IncomeUpgrade : Upgrade
    {
        public event Action<int> ChangedIncome;

        private void Start()
        {
            if (PlayerPrefs.HasKey("IncomeUpgrade"))
                LoadProgress("IncomeUpgrade");
            else
                CurrentPrice = _price;

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
