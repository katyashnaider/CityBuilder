using CityBuilder.Sounds;
using TMPro;
using UnityEngine;

namespace CityBuilder.SDK
{
    public class Advertisement : MonoBehaviour
    {
        [SerializeField] private TMP_Text _timerText;
        [SerializeField] private GameObject _advertisementObject;

        private const float AdInterval = 180f; //180
        
        private float _lastAdDisplayTime;
        private float _countdownTime = 3f;
        private bool _countdownStarted = false;

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
            float timeSinceLastAd = Time.time - _lastAdDisplayTime;

            if (timeSinceLastAd >= AdInterval)
            {
                if (!_countdownStarted)
                {
                    StartCountdown();
                }
                else if (_countdownTime <= 0f)
                {
                    ShowAdvertisement();
                    _lastAdDisplayTime = Time.time;
                    _countdownStarted = false;
                }
            }
            else
            {
                ResetCountdown();
            }

            if (_countdownStarted)
            {
                UpdateCountdown();
            }
        }

        private void StartCountdown()
        {
            _countdownStarted = true;
            _countdownTime = 3f;
        }

        private void ResetCountdown()
        {
            _countdownStarted = false;
        }

        private void UpdateCountdown()
        {
            _countdownTime -= Time.deltaTime;
            _advertisementObject.SetActive(true);
            _timerText.text = _countdownTime.ToString("0");
        }

        private void ShowAdvertisement()
        {
            _advertisementObject.gameObject.SetActive(false);
            Agava.YandexGames.InterstitialAd.Show(OnOpenCallback, OnCloseCallback);
        }

        private void OnOpenCallback()
        {
            Time.timeScale = 0;
            SoundManager.Instance.MuteSound(true);
        }

        private void OnCloseCallback(bool wasShown)
        {
            if (wasShown)
            {
                Time.timeScale = 1;
                SoundManager.Instance.MuteSound(false);
            }
        }
    }
}