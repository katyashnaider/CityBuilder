using System;
using UnityEngine;

namespace CityBuilder.Upgrade
{
    public class SpeedUpgrade : Upgrade
    {
        public event Action<float> ChangedSpeed;

        private void Start()
        {
            if (PlayerPrefs.HasKey("SpeedUpgrade"))
            {
                LoadProgress("SpeedUpgrade");
            }

            UpgradeInfo();
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
    }
}