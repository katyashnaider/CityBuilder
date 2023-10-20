using UnityEngine;

namespace Scripts.Level
{
    public class LevelDataModel
    {
        //private LevelDataView _levelDataView;
        private int _levelNumber;

        private const int MaxLevel = 10;

        public LevelDataModel(int levelNumber) =>
            _levelNumber = levelNumber;

        public void AdvanceNextLevel()
        {
            _levelNumber++;
            PlayerPrefs.SetInt("LevelNumber", _levelNumber);

            if (_levelNumber == MaxLevel)
                _levelNumber = 0;
        }
    }
}