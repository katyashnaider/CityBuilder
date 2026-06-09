using CityBuilder.Building;
using CityBuilder.Sounds;
using CityBuilder.Workers;
using CityBuilder.Workers.Wallet;
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

        [Header("View")] [SerializeField] private GameObject _levelCompletedScreen;
        [SerializeField] private FactoryWorker _factoryWorker;
        [SerializeField] private AudioClip _soundEffect;
        [SerializeField] private GameObject[] _buttons;

        private Coroutine _coroutine;
        private int _currentIndex;

        private LevelDataView _levelDataView;
        private LevelDataModel _levelDataVModel;

        private int _levelNumber = 2;
        
        private void Awake()
        {
            _levelDataVModel = new LevelDataModel(_levelNumber);
            _levelDataView = new LevelDataView(_levelCompletedScreen, _buttons, _factoryWorker);

            _levelNumber = PlayerPrefs.HasKey("LevelNumber") ? _levelDataVModel.LoadProgress("LevelNumber") : 2;
        }

        private void OnEnable()
        {
            _building.ConstructedBuilding += OnConstructedBuilding;
        }

        private void OnDisable()
        {
            _building.ConstructedBuilding -= OnConstructedBuilding;
        }

        public void OnClickNextLevel()
        {
            _gameplay.RestartGame();
            SceneManager.LoadScene(_levelNumber);
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