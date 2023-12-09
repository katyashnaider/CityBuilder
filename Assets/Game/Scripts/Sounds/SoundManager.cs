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
        }

        public void PlaySoundMainMenu()
        {
            _musicSoundMainMenu.Play();
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
        }
    }
}