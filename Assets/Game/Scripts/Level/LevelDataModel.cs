namespace CityBuilder.Level
{
    internal sealed class LevelDataModel
    {
        private const int MaxLevel = 5;
        
        private int _levelNumber;

        public LevelDataModel(int levelNumber)
        {
            _levelNumber = levelNumber;
        }

        private void SaveProgress(string key)
        {
            ProgressHandler progressHandler = new ProgressHandler();

            ProgressHandler.Save saveData = new ProgressHandler.Save
            {
                LevelNumber = _levelNumber
            };

            progressHandler.SaveProgress(key, saveData);
        }

        public int AdvanceNextLevel()
        {
            _levelNumber++;

            if (_levelNumber > MaxLevel)
            {
                _levelNumber = 1;
            }

            SaveProgress("LevelNumber");

            return _levelNumber;
        }

        public int LoadProgress(string key)
        {
            ProgressHandler progressHandler = new ProgressHandler();
            ProgressHandler.Save loadedData = progressHandler.LoadProgress(key);

            return _levelNumber = loadedData.LevelNumber;
        }
    }
}