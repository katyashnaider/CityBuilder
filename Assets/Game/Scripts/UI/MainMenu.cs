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
            _levelNumber = PlayerPrefs.HasKey("LevelNumber") ? PlayerPrefs.GetInt("LevelNumber", _levelNumber) : 1;

            _buildings[_levelNumber - 1].gameObject.SetActive(true);
            SoundManager.Instance.PlaySoundMainMenu(_clipMainMenu);
        }

        public void PlayGame()
        {
            _loading.LoadScene(_levelNumber);
            
            SoundManager.Instance.StopSoundMainMenu();
            SoundManager.Instance.PlaySoundGame(_clipGame);
        }
    }
}