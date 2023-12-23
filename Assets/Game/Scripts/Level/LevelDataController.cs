using CityBuilder.Building;
using CityBuilder.Sounds;
using CityBuilder.Worker;
using CityBuilder.Worker.Wallet;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CityBuilder.Level
{
    public class LevelDataController : MonoBehaviour
    {
        [Header("Controller")]
        [SerializeField] private Gameplay _gameplay;
        [SerializeField] private BuildingController _building;
        [SerializeField] private Button _buttonX2Coins;
        [SerializeField] private Wallet _wallet;
        
        [Header("View")]
        [SerializeField] private GameObject _levelCompletedScreen;
        [SerializeField] private FactoryWorker _factoryWorker;
        [SerializeField] private GameObject _buttons;
        [SerializeField] private AudioClip _soundEffect;
       
        private Coroutine _coroutine;
        private int _currentIndex;
        
        private LevelDataView _levelDataView;
        private LevelDataModel _levelDataVModel;

        private int _levelNumber = 2;
        
        private const int AmountOfDoubling = 1000;

        private void Awake()
        {
            _levelDataVModel = new LevelDataModel(_levelNumber);
            _levelDataView = new LevelDataView(_levelCompletedScreen, _buttons, _factoryWorker);

            _levelNumber = PlayerPrefs.HasKey("LevelNumber") ? _levelDataVModel.LoadProgress("LevelNumber") : 2;
        }

        private void OnEnable()
        {
            _building.ConstructedBuilding += OnConstructedBuilding;
            _buttonX2Coins.onClick.AddListener(OnX2Coins);
        }

        private void OnDisable()
        {
            _building.ConstructedBuilding -= OnConstructedBuilding;
            _buttonX2Coins.onClick.AddListener(OnX2Coins);
        }

        public void OnClickNextLevel()
        {
            Agava.YandexGames.InterstitialAd.Show(OnOpenCallback, OnCloseCallback);
            _gameplay.RestartGame();
            SceneManager.LoadScene(_levelNumber);
        }

        private void OnX2Coins()
        {
            Agava.YandexGames.VideoAd.Show(OnOpenCallback, OnRewardedCallback, OnCloseCallback);
        }

        private void OnOpenCallback()
        {
            Time.timeScale = 0;
            SoundManager.Instance.StopSoundGame();
        } 
        
        private void OnRewardedCallback()
        {
            _wallet.AddCoins(AmountOfDoubling);
        }
        
        private void OnCloseCallback()
        {
            Time.timeScale = 1;
            SoundManager.Instance.PlaySoundGame();
            _buttonX2Coins.interactable = false;
        }
        
        private void OnCloseCallback(bool wasShown)
        {
            if (wasShown)
            {
                Time.timeScale = 1;
                SoundManager.Instance.PlaySoundGame();
            }
        }
        
        private void OnConstructedBuilding()
        {
            _levelNumber = _levelDataVModel.AdvanceNextLevel();

            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }

            _coroutine = StartCoroutine(_levelDataView.ShowLevelCompletedScreen(true, _soundEffect));
        }
    }
}