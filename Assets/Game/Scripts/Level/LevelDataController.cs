using Scripts.Building;
using UnityEngine;
using UnityEngine.SceneManagement;
using Workers;

namespace Scripts.Level
{
    public class LevelDataController : MonoBehaviour
    {
        [SerializeField] private BuildingController _buildingController;
        [SerializeField] private GameObject _levelCompletedScreen;
        [SerializeField] private GameObject _buttons;
        [SerializeField] private FactoryWorker _factoryWorker;
        [SerializeField] private AudioClip _soundEffect;

        private LevelDataModel _levelDataVModel;
        private LevelDataView _levelDataView;

        private int _levelNumber = 1;
        private int _currentIndex;

        private void Awake()
        {
            _levelDataVModel = new LevelDataModel(_levelNumber);
            _levelDataView = new LevelDataView(_levelCompletedScreen, _buttons, _factoryWorker);
        }

        private void OnEnable() =>
            _buildingController.ConstructedBuilding += OnConstructedBuildingController;

        private void OnDisable() =>
            _buildingController.ConstructedBuilding -= OnConstructedBuildingController;

        public void OnClickNextLevel()
        {
            _levelNumber = PlayerPrefs.GetInt("LevelNumber", _levelNumber);
          //  _currentIndex = 0;
          //  PlayerPrefs.SetInt("CurrentIndex", _currentIndex);
            //_currentIndex = PlayerPrefs.GetInt("CurrentIndex", _currentIndex);
          //  print(_levelNumber);
            SceneManager.LoadScene(_levelNumber);
        }

        private void OnConstructedBuildingController()
        {
            _levelDataVModel.AdvanceNextLevel();
            StartCoroutine(_levelDataView.ShowLevelCompletedScreen(true, _soundEffect));
        }
    }
}