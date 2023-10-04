using UnityEngine;

namespace Scripts.UI
{
    internal sealed class MainMenu : MonoBehaviour
    {
        [SerializeField] private GameObject[] _buildings;
        [SerializeField] private Loading _loading;

        private int _levelNumber = 0;

        private void Start()
        {
            _levelNumber = PlayerPrefs.HasKey("LevelNumber") ? PlayerPrefs.GetInt("LevelNumber", _levelNumber) : 1;

            _buildings[_levelNumber - 1].gameObject.SetActive(true);
        }

        public void PlayGame() =>
            _loading.LoadScene(_levelNumber);
    }
}