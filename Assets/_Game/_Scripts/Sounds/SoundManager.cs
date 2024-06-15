using UnityEngine;

namespace CityBuilder.Sounds
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private AudioSource _musicSoundMainMenu;
        [SerializeField] private AudioSource _musicSoundGame;
        [SerializeField] private AudioSource _effectSource;

        public static SoundManager Instance;

        private bool _userMuted;
        private bool _adMuted;

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

            // Initialize userMuted with current mute state
            _userMuted = _musicSoundMainMenu.mute;
            _adMuted = false;
        }

        public void PlaySoundMainMenu()
        {
            _musicSoundMainMenu.Play();
        }

        public void MuteSound(bool mute)
        {
            _adMuted = mute;
            ApplyMuteState();
        }

        private void ApplyMuteState()
        {
            bool finalMuteState = _userMuted || _adMuted;
            _musicSoundMainMenu.mute = finalMuteState;
            _musicSoundGame.mute = finalMuteState;
            _effectSource.mute = finalMuteState;
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
            _userMuted = !_userMuted;
            ApplyMuteState();
            SaveMuteSound();
        }

        private void SaveMuteSound()
        {
            PlayerPrefs.SetInt("MuteSoundMainMenu", _userMuted ? 1 : 0);
            PlayerPrefs.SetInt("MuteSoundGame", _userMuted ? 1 : 0);
            PlayerPrefs.SetInt("MuteSoundEffect", _userMuted ? 1 : 0);
            PlayerPrefs.Save();
        }

        private void LoadMuteSound()
        {
            _musicSoundMainMenu.mute = PlayerPrefs.GetInt("MuteSoundMainMenu") == 1;
            _musicSoundGame.mute = PlayerPrefs.GetInt("MuteSoundGame") == 1;
            _effectSource.mute = PlayerPrefs.GetInt("MuteSoundEffect") == 1;

            _userMuted = _musicSoundMainMenu.mute;
        }

        private void SetDefaultMuteSound()
        {
            _musicSoundMainMenu.mute = false;
            _musicSoundGame.mute = false;
            _effectSource.mute = false;

            _userMuted = false;
        }
    }
}