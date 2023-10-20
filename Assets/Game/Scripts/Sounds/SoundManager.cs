using UnityEngine;

namespace Scripts.Sounds
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance;

        [SerializeField] private AudioSource _musicSoundMainMenu;
        [SerializeField] private AudioSource _musicSoundGame;
        [SerializeField] private AudioSource _effectSource;
        
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

        public void PlaySoundMainMenu(AudioClip clip) => 
            _musicSoundMainMenu.PlayOneShot(clip);
        
        public void PlaySoundGame(AudioClip clip) => 
            _musicSoundGame.PlayOneShot(clip);
        
        public void StopSoundMainMenu() => 
            _musicSoundMainMenu.Stop();
        
        public void StopSoundGame() => 
            _musicSoundGame.Stop();

        public void PlaySoundEffect(AudioClip clip) => 
            _effectSource.PlayOneShot(clip);

        public void ToggleMusic()
        {
            _musicSoundMainMenu.mute = !_musicSoundMainMenu.mute;
            _musicSoundGame.mute = !_musicSoundGame.mute;
            _effectSource.mute = !_effectSource.mute;
        }
    }
}