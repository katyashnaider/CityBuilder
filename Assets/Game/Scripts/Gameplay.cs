using DG.Tweening;
using UnityEngine;

namespace CityBuilder
{
    public class Gameplay : MonoBehaviour
    {
        [SerializeField] private Upgrade.Upgrade[] _upgrades;

        private RestartEntity[] _restartEntities;

        private void Awake()
        {
            _restartEntities = FindObjectsOfType<RestartEntity>(true);
        }

        public void TogglePause(bool isEnabled)
        {
            Time.timeScale = isEnabled ? 0 : 1;
        }

        public void RestartGame()
        {
            if (_restartEntities == null && _upgrades.Length == 0)
            {
                Debug.LogError("Components Restart Entity and Upgrades is null!");
                return;
            }

            foreach (RestartEntity restartEntity in _restartEntities)
            {
                restartEntity.Restart();
            }

            foreach (Upgrade.Upgrade upgrade in _upgrades)
            {
                upgrade.RestartLevel();
            }

            DOTween.KillAll();
        }
    }
}