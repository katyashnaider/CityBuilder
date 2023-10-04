using System;
using UnityEngine;

namespace Scripts.Sounds
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance;

        [SerializeField] private AudioSource _musicSource;
        
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

        public void PlaySound(AudioClip clip) => 
            _musicSource.PlayOneShot(clip);

        public void ToggleMusic() => 
            _musicSource.mute = !_musicSource.mute;
    }
}