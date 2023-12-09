using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace CityBuilder.Building
{
    public sealed class BuildingController : RestartEntity
    {
        [Header("References")]
        [SerializeField] private BuildingPartSettings _partSettings;
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private ViewCoins _viewCoinsPrefab;
        [SerializeField] private AudioClip _clip;

        [Header("Building Parts")]
        [SerializeField] private BuildingPart[] _buildingsParts;

        private Transform _createdCanvasCoins;

        private int _currentIndex;

        public event Action DeliveredStone;
        public event Action ConstructedBuilding;

        private void Awake()
        {
            if (PlayerPrefs.HasKey("CurrentIndex"))
            {
                _currentIndex = LoadProgress("CurrentIndex");
            }

            for (int i = 0; i < _currentIndex; i++)
            {
                _buildingsParts[i].gameObject.SetActive(true);
            }

            _createdCanvasCoins = Instantiate(_viewCoinsPrefab.transform, transform);

            CanvasGroup canvasGroup = _createdCanvasCoins.GetComponent<CanvasGroup>();

            foreach (BuildingPart part in _buildingsParts)
            {
                part.Construct(_partSettings, _createdCanvasCoins, canvasGroup, _viewCoinsPrefab, _particleSystem, _clip);
            }
        }

        public void GetStone()
        {
            if (_currentIndex >= 3) // заменить число на _buildingsParts.Length
            {
                ConstructedBuilding?.Invoke();
            }
            else
            {
                DeliveredStone?.Invoke();

                _buildingsParts[_currentIndex].Active();
                _currentIndex++;

                SaveProgress("CurrentIndex");
            }
        }

        public void SetCurrentPrice(int price)
        {
            foreach (BuildingPart part in _buildingsParts)
            {
                part.InjectPrice(price);
            }
        }

        public override void Restart()
        {
            _currentIndex = 0;
            SaveProgress("CurrentIndex");

            foreach (BuildingPart part in _buildingsParts)
            {
                part.gameObject.SetActive(false);
            }
        }

        private void SaveProgress(string key)
        {
            ProgressHandler progressHandler = new ProgressHandler();

            ProgressHandler.Save saveData = new ProgressHandler.Save
            {
                CurrentIndex = _currentIndex
            };

            progressHandler.SaveProgress(key, saveData);
        }

        private int LoadProgress(string key)
        {
            ProgressHandler progressHandler = new ProgressHandler();
            ProgressHandler.Save loadedData = progressHandler.LoadProgress(key);

            return _currentIndex = loadedData.CurrentIndex;
        }

        [ContextMenu("Order")]
        private void OrderParts()
        {
            _buildingsParts = GetComponentsInChildren<BuildingPart>(true)
                .OrderBy(x => x.transform.position.x).OrderBy(z => z.transform.position.z)
                .OrderBy(y => y.transform.position.y).ToArray();
        }

        [ContextMenu("RemoveDuplicate")]
        private void RemoveParts()
        {
            BuildingPart[] buildingsParts = GetComponentsInChildren<BuildingPart>().OrderBy(x => x.transform.position.y).ToArray();

            for (int i = 0; i < buildingsParts.Length; i++)
            {
                if (buildingsParts[i] is null)
                {
                    continue;
                }

                for (int j = 0; j < buildingsParts.Length; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }

                    if (buildingsParts[j] is null)
                    {
                        continue;
                    }

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
            BuildingPart[] parts = GetComponentsInChildren<BuildingPart>(true);

            foreach (BuildingPart part in parts)
            {
                part.gameObject.SetActive(!part.gameObject.activeSelf);
            }
        }
    }
}