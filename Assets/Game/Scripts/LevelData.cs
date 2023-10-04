using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Game.Scripts
{
    public sealed class LevelData : MonoBehaviour
    {
        [SerializeField] private Building _building;
        [SerializeField] private GameObject _levelCompletedScreen;

        private int _levelNumber = 1;

        private const int MaxLevel = 10;

        private void OnEnable() =>
            _building.ConstructedBuilding += OnConstructedBuilding;

        private void OnDisable() =>
            _building.ConstructedBuilding -= OnConstructedBuilding;

        public void OnClickNextLevel() =>
            SceneManager.LoadScene(_levelNumber);

        private void OnConstructedBuilding()
        {
            Time.timeScale = 1;
            _levelNumber++;
            PlayerPrefs.SetInt("LevelNumber", _levelNumber);
            _levelCompletedScreen.SetActive(true);

            if (_levelNumber == MaxLevel)
                _levelNumber = 0;
        }
    }
}