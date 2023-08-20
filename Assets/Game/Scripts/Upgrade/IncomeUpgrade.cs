using System;
using UnityEngine;

namespace Upgrades
{
    public class IncomeUpgrade : Upgrade
    {
        public event Action<int> ChangedIncome;

        public override void ApplyUpgrade()
        {
            if (_wallet.HasEnoughCoins(CurrentPrice))
            {
                base.ApplyUpgrade();
                ChangedIncome?.Invoke(_upgradeAmount);
            }
        }
    }
}
