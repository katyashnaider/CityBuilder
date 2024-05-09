using UnityEngine;

namespace CityBuilder.Sounds
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private AudioSource _musicSoundMainMenu;
        [SerializeField] private AudioSource _musicSoundGame;
        [SerializeField] private AudioSource _effectSource;

        public static SoundManager Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            if (PlayerPrefs.HasKey("MuteSoundMainMenu") || PlayerPrefs.HasKey("MuteSoundGame") ||
                PlayerPrefs.HasKey("MuteSoundEffect"))
            {
                LoadMuteSound();
            }
            else
            {
                SetDefaultMuteSound();
            }
        }

        public void PlaySoundMainMenu()
        {
            _musicSoundMainMenu.Play();
        } 
        
        public void MuteSound(bool mute)
        {
            LoadMuteSound();
            
            if (_musicSoundMainMenu.mute && _musicSoundGame.mute && _effectSource.mute)
                return;
            
            _musicSoundMainMenu.mute = mute;
            _musicSoundGame.mute = mute;
            _effectSource.mute = mute;
        }

        public void PlaySoundGame()
        {
            _musicSoundGame.Play();
        }

        public void StopSoundMainMenu()
        {
            _musicSoundMainMenu.Stop();
        }

        public void StopSoundGame()
        {
            _musicSoundGame.Stop();
        }

        public void PlaySoundEffect(AudioClip clip)
        {
            _effectSource.PlayOneShot(clip);
        }

        public void ToggleMusic()
        {
            _musicSoundMainMenu.mute = !_musicSoundMainMenu.mute;
            _musicSoundGame.mute = !_musicSoundGame.mute;
            _effectSource.mute = !_effectSource.mute;

            SaveMuteSound();
        }

        private void SaveMuteSound()
        {
            PlayerPrefs.SetInt("MuteSoundMainMenu", _musicSoundMainMenu.mute ? 1 : 0);
            PlayerPrefs.SetInt("MuteSoundGame", _musicSoundGame.mute ? 1 : 0);
            PlayerPrefs.SetInt("MuteSoundEffect", _effectSource.mute ? 1 : 0);

            PlayerPrefs.Save();
        }

        private void LoadMuteSound()
        {
            _musicSoundMainMenu.mute = PlayerPrefs.GetInt("MuteSoundMainMenu") == 1;
            _musicSoundGame.mute = PlayerPrefs.GetInt("MuteSoundGame") == 1;
            _effectSource.mute = PlayerPrefs.GetInt("MuteSoundEffect") == 1;
        }

        private void SetDefaultMuteSound()
        {
            _musicSoundMainMenu.mute = false;
            _musicSoundGame.mute = false;
            _effectSource.mute = false;
        }
    }
}