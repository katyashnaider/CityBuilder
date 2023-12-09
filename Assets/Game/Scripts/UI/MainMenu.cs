using CityBuilder.Sounds;
using UnityEngine;

namespace CityBuilder.UI
{
    internal sealed class MainMenu : MonoBehaviour
    {
        [SerializeField] private Canvas _fps;
        [SerializeField] private GameObject[] _buildings;
        [SerializeField] private Loading _loading;
        
        private bool _isOpen;
        private int _levelNumber;

        private void Start()
        {
            _levelNumber = PlayerPrefs.HasKey("LevelNumber") ? LoadProgress("LevelNumber") : 1;

            if (_levelNumber <= _buildings.Length)
            {
                _buildings[_levelNumber - 1].gameObject.SetActive(true);
            }
            else
            {
                Debug.LogError("_levelNumber " + "(" + _levelNumber + ")" + " is outside the array index _buildings" + "(" + _buildings.Length + ")");
            }

            SoundManager.Instance.PlaySoundMainMenu();
        }

        public void PlayGame()
        {
            _loading.LoadScene(_levelNumber);

            SoundManager.Instance.StopSoundMainMenu();
            SoundManager.Instance.PlaySoundGame();
        }

        public void OnClickButtonFps()
        {
            _fps.gameObject.SetActive(_isOpen = !_isOpen);
        }

        private int LoadProgress(string key)
        {
            ProgressHandler progressHandler = new ProgressHandler();
            ProgressHandler.Save loadedData = progressHandler.LoadProgress(key);

            return _levelNumber = loadedData.LevelNumber;
        }
    }
}