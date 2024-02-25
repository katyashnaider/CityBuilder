using CityBuilder.Sounds;
using UnityEngine;

namespace CityBuilder.SDK
{
    public class Advertisement : MonoBehaviour
    {
        private float _lastAdDisplayTime;
        private readonly float _adInterval = 180f;


        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            _lastAdDisplayTime = Time.time;
        }

        private void Update()
        {
            if (Time.time - _lastAdDisplayTime >= _adInterval)
            {
                ShowAdvertisement();

                _lastAdDisplayTime = Time.time;
            }
        }

        private void ShowAdvertisement()
        {
            Agava.YandexGames.InterstitialAd.Show(OnOpenCallback, OnCloseCallback);
        }

        private void OnOpenCallback()
        {
            Time.timeScale = 0;
            SoundManager.Instance.StopSoundGame();
        }

        private void OnCloseCallback(bool wasShown)
        {
            if (wasShown)
            {
                Time.timeScale = 1;
                SoundManager.Instance.PlaySoundGame();
            }
        }
    }
}