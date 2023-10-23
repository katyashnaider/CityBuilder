using Scripts.Building;
using UnityEngine;
using UnityEngine.SceneManagement;
using Workers;

namespace Scripts.Level
{
    public class LevelDataController : MonoBehaviour
    {
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private BuildingController _building;
        [SerializeField] private GameObject _levelCompletedScreen;
        [SerializeField] private GameObject _buttons;
        [SerializeField] private FactoryWorker _factoryWorker;
        [SerializeField] private AudioClip _soundEffect;

        private LevelDataModel _levelDataVModel;
        private LevelDataView _levelDataView;

        private int _levelNumber = 1;
        private int _currentIndex;
        private Coroutine _coroutine;

        private void Awake()
        {
            _levelDataVModel = new LevelDataModel(_levelNumber);
            _levelDataView = new LevelDataView(_levelCompletedScreen, _buttons, _factoryWorker);

            _levelNumber = PlayerPrefs.HasKey("LevelNumber") ? _levelDataVModel.LoadProgress("LevelNumber") : 1;
        }

        private void OnEnable() =>
            _building.ConstructedBuilding += OnConstructedBuilding;

        private void OnDisable() =>
            _building.ConstructedBuilding -= OnConstructedBuilding;

        public void OnClickNextLevel()
        {
            _gameManager.RestartGame();
            SceneManager.LoadScene(_levelNumber);
        }

        private void OnConstructedBuilding()
        {
            _levelNumber = _levelDataVModel.AdvanceNextLevel();

            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(_levelDataView.ShowLevelCompletedScreen(true, _soundEffect));
        }
    }
}