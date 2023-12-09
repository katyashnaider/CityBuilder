using System;
using UnityEngine;

namespace CityBuilder
{
    public class ProgressHandler
    {
        public void SaveProgress(string key, Save saveData)
        {
            string json = JsonUtility.ToJson(saveData);
            PlayerPrefs.SetString(key, json);
        }

        public Save LoadProgress(string key)
        {
            string json = PlayerPrefs.GetString(key);

            if (PlayerPrefs.HasKey(key))
            {
                return JsonUtility.FromJson<Save>(json);
            }

            Save save = new Save();
            SaveProgress(key, save);
            return save;
        }

        [Serializable]
        public struct Save
        {
            public int LevelUpgrade;
            public int LevelNumber;
            public int Price;
            public int Wallet;
            public int CurrentIndex;
        }
    }
}