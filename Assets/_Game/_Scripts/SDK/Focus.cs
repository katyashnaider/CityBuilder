using Agava.WebUtility;
using CityBuilder.Sounds;
using UnityEngine;

namespace CityBuilder.SDK
{
    public class Focus : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource1;
        [SerializeField] private AudioSource _audioSource2;
        [SerializeField] private AudioSource _audioSource3;
        
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void OnEnable()
        {
            Application.focusChanged += OnInBackgroundChangeApp;
            WebApplication.InBackgroundChangeEvent += OnInBackgroundChangeWeb;
        }

        private void OnDisable()
        {
            Application.focusChanged -= OnInBackgroundChangeApp;
            WebApplication.InBackgroundChangeEvent -= OnInBackgroundChangeWeb;
        }

        private void OnInBackgroundChangeApp(bool inApp)
        {
            Debug.Log("App focus changed: " + inApp);
            MuteAudio(!inApp);
            PauseGame(!inApp);
        }

        private void OnInBackgroundChangeWeb(bool isBackground)
        {
            Debug.Log("Web focus changed: " + isBackground);
            MuteAudio(isBackground);
            PauseGame(isBackground);
        }

        private void MuteAudio(bool mute)
        {
            Debug.Log("Muting audio: " + mute);
            _audioSource1.volume = mute ? 0 : 1;
            _audioSource2.volume = mute ? 0 : 1;
            _audioSource3.volume = mute ? 0 : 1;
            //SoundManager.Instance.MuteSound(mute);
        }

        private void PauseGame(bool value)
        {
            Debug.Log("Pausing game: " + value);
            Time.timeScale = value ? 0 : 1;
        }
    }
}