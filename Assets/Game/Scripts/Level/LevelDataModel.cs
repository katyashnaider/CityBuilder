using UnityEngine;

namespace Scripts.Level
{
    internal sealed class LevelDataModel
    {
        private int _levelNumber;

        private const int MaxLevel = 10;

        public LevelDataModel(int levelNumber) =>
            _levelNumber = levelNumber;


        private void SaveProgress(string key)
        {
            var progressHandler = new ProgressHandler();

            var saveData = new ProgressHandler.Save
            {
                LevelNumber = _levelNumber
            };

            progressHandler.SaveProgress(key, saveData);
        }
        public int AdvanceNextLevel()
        {
            _levelNumber++;
            SaveProgress("LevelNumber");
            
            // PlayerPrefs.SetInt("LevelNumber", _levelNumber);
            if (_levelNumber == MaxLevel)
                _levelNumber = 0;

            return _levelNumber;
        }

        public int LoadProgress(string key)
        {
            var progressHandler = new ProgressHandler();
            var loadedData = progressHandler.LoadProgress(key);

            return _levelNumber = loadedData.LevelNumber;
        }
    }
}