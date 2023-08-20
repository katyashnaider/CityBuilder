using System;
using UnityEngine;

namespace Upgrades
{
    public class AddWorkerUpgrade : Upgrade
    {
        private void Start()
        {
            //if (PlayerPrefs.HasKey("SpeedUpgrade"))
            //    LoadProgress("SpeedUpgrade");
            //else
            CurrentPrice = _price;

            UpgradeInfo();
        }

        public override void ApplyUpgrade()
        {
            if (_wallet.HasEnoughCoins(CurrentPrice))
            {
                base.ApplyUpgrade();
                // SaveProgress("SpeedUpgrade");
                // ChangedSpeed?.Invoke(_upgradeAmount);
            }
        }
    }
}
