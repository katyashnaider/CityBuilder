using Agava.WebUtility;
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
            MuteAudio(!inApp);
            PauseGame(!inApp);
        }

        private void OnInBackgroundChangeWeb(bool isBackground)
        {
            MuteAudio(isBackground);
            PauseGame(isBackground);
        }

        private void MuteAudio(bool mute)
        {
            _audioSource1.volume = mute ? 0 : 1;
            _audioSource2.volume = mute ? 0 : 1;
            _audioSource3.volume = mute ? 0 : 1;
            //SoundManager.Instance.MuteSound(mute);
        }

        private void PauseGame(bool value)
        {
            Time.timeScale = value ? 0 : 1;
        }
    }
}