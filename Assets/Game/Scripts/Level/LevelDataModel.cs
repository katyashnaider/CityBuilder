namespace Scripts.Level
{
    internal sealed class LevelDataModel
    {
        //private LevelDataView _levelDataView;
        private int _levelNumber;

        private const int MaxLevel = 10;

        public LevelDataModel(int levelNumber) =>
            _levelNumber = levelNumber;

        public void AdvanceNextLevel()
        {
            _levelNumber++;
            SaveProgress("LevelNumber");
            //PlayerPrefs.SetInt("LevelNumber", _levelNumber);

            if (_levelNumber == MaxLevel)
                _levelNumber = 0;
        }

        private void SaveProgress(string key)
        {
            var progressHandler = new ProgressHandler();

            var saveData = new ProgressHandler.Save
            {
                LevelNumber = _levelNumber
            };

            progressHandler.SaveProgress(key, saveData);
        }
        
        public void LoadProgress(string key)
        {
            var progressHandler = new ProgressHandler();
            var loadedData = progressHandler.LoadProgress(key);

            _levelNumber = loadedData.CurrentIndex;
        }
    }
}