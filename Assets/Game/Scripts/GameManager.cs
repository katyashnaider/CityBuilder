using DG.Tweening;
using UnityEngine;

namespace Scripts
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Upgrades.Upgrade[] _upgrades;

        private RestartEntity[] _restartEntities;

        private void Awake() => _restartEntities = FindObjectsOfType<RestartEntity>(true);

        public void TogglePause(bool isEnabled) => Time.timeScale = isEnabled ? 0 : 1;

        public void RestartGame()
        {
            if (_restartEntities == null && _upgrades.Length == 0)
            {
                Debug.LogError("Components Restart Entity and Upgrades is null!");
                return;
            }

            foreach (var restartEntity in _restartEntities)
                restartEntity.Restart();

            foreach (var upgrade in _upgrades)
                upgrade.RestartLevel();

            DOTween.KillAll();
        }
    }
}