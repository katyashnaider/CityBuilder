using System;
using System.Linq;
using Scripts.Building;
using UnityEngine;

namespace Scripts.Building
{
    public sealed class BuildingController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private BuildingPartSettings _partSettings;
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private Transform _canvasCoins;
        [SerializeField] private AudioClip _clip;

        [Header("Building Parts")]
        [SerializeField] private BuildingPart[] _buildingsParts;
        
        private int _currentCountStones = 0;
        private int _currentIndex = 0;
        private Transform _currentStoneTransform;
        private Transform _createdCanvasCoins;
        private int _currentPrice;

        private const int StonesNeededNumber = 1;
        
        public event Action DeliveredStone;
        public event Action ConstructedBuilding;

        private void Awake()
        {
            if (PlayerPrefs.HasKey("CurrentIndex"))
                LoadProgress("CurrentIndex");
                //_currentIndex = PlayerPrefs.GetInt("CurrentIndex", _currentIndex);

            for (int i = 0; i < _currentIndex; i++)
                _buildingsParts[i].gameObject.SetActive(true);

            _createdCanvasCoins = Instantiate(_canvasCoins, transform);

            foreach (var part in _buildingsParts)
                part.Construct(_partSettings, _createdCanvasCoins, _createdCanvasCoins.GetComponent<CanvasGroup>(),
                    _createdCanvasCoins.GetComponent<ViewCoins>(), _particleSystem, _clip);
        }

        public void GetStone()
        {
            if (_currentIndex >= 3) // заменить число на _buildingsParts.Length
                ConstructedBuilding?.Invoke();

            _currentCountStones++;
            DeliveredStone?.Invoke();

            if (_currentCountStones == StonesNeededNumber)
            {
                _buildingsParts[_currentIndex].Active();
                _currentIndex++;
                _currentCountStones = 0;
            }

            //PlayerPrefs.SetInt("CurrentIndex", _currentIndex);
            SaveProgress("CurrentIndex");
        }

        public void SetCurrentPrice(int price)
        {
            foreach (BuildingPart part in _buildingsParts)
                part.InjectPrice(price);
        }

        private void SaveProgress(string key)
        {
            var progressHandler = new ProgressHandler();

            var saveData = new ProgressHandler.Save
            {
                CurrentIndex = _currentIndex
            };

            progressHandler.SaveProgress(key, saveData);
        }
        
        private void LoadProgress(string key)
        {
            var progressHandler = new ProgressHandler();
            var loadedData = progressHandler.LoadProgress(key);

            _currentIndex = loadedData.CurrentIndex;
        }
        
        [ContextMenu("Order")]
        private void OrderParts() =>
            _buildingsParts = GetComponentsInChildren<BuildingPart>(true)
                .OrderBy(x => x.transform.position.x).OrderBy(z => z.transform.position.z)
                .OrderBy(y => y.transform.position.y).ToArray();
        
        [ContextMenu("RemoveDuplicate")]
        private void RemoveParts()
        {
            var buildingsParts = GetComponentsInChildren<BuildingPart>().OrderBy(x => x.transform.position.y).ToArray();
        
            for (int i = 0; i < buildingsParts.Length; i++)
            {
                if (buildingsParts[i] == null) continue;
        
                for (int j = 0; j < buildingsParts.Length; j++)
                {
                    if (i == j) continue;
        
                    if (buildingsParts[j] == null) continue;
        
                    if (buildingsParts[i].transform.position == buildingsParts[j].transform.position)
                    {
                        DestroyImmediate(buildingsParts[j].gameObject);
                    }
                }
            }
        }
        
        [ContextMenu("OnObject")]
        private void OnObject()
        {
            var parts = GetComponentsInChildren<BuildingPart>(true);
        
            foreach (var part in parts)
                part.gameObject.SetActive(!part.gameObject.activeSelf);
        }
    }
}