using CityBuilder.Building;
using CityBuilder.Worker;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace CityBuilder.Level
{
    public class LevelDataController : MonoBehaviour
    {
        [SerializeField] private Gameplay _gameplay;
        [SerializeField] private BuildingController _building;
        [SerializeField] private GameObject _levelCompletedScreen;
        [SerializeField] private GameObject _buttons;
        [SerializeField] private FactoryWorker _factoryWorker;
        [SerializeField] private AudioClip _soundEffect;
       
        private Coroutine _coroutine;
        private int _currentIndex;
        
        private LevelDataView _levelDataView;
        private LevelDataModel _levelDataVModel;

        private int _levelNumber = 1;

        private void Awake()
        {
            _levelDataVModel = new LevelDataModel(_levelNumber);
            _levelDataView = new LevelDataView(_levelCompletedScreen, _buttons, _factoryWorker);

            _levelNumber = PlayerPrefs.HasKey("LevelNumber") ? _levelDataVModel.LoadProgress("LevelNumber") : 1;
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