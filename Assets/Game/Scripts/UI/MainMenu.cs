using Scripts.Sounds;
using UnityEngine;

namespace Scripts.UI
{
    internal sealed class MainMenu : MonoBehaviour
    {
        [SerializeField] private GameObject[] _buildings;
        [SerializeField] private Loading _loading;
        [SerializeField] private AudioClip _clipMainMenu;
        [SerializeField] private AudioClip _clipGame;

        private int _levelNumber = 0;

        private void Start()
        {
            _levelNumber = PlayerPrefs.HasKey("LevelNumber") ? LoadProgress("LevelNumber") : 1;

            if (_levelNumber <= _buildings.Length)
                _buildings[_levelNumber - 1].gameObject.SetActive(true);
            else
                Debug.LogError("_levelNumber " + "(" + _levelNumber + ")" + " is outside the array index _buildings" + "(" + _buildings.Length + ")");

            SoundManager.Instance.PlaySoundMainMenu(_clipMainMenu);
        }

        public void PlayGame()
        {
            _loading.LoadScene(_levelNumber);

            SoundManager.Instance.StopSoundMainMenu();
            SoundManager.Instance.PlaySoundGame(_clipGame);
        }
        
        private int LoadProgress(string key)
        {
            var progressHandler = new ProgressHandler();
            var loadedData = progressHandler.LoadProgress(key);

            return _levelNumber = loadedData.LevelNumber;
        }
    }
}