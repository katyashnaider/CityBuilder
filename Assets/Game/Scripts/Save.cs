using System;
using UnityEngine;

public class ProgressHandler
{
    // public ProgressHandler()
    // {
    //     LoadProgress("LevelNumber");
    // }

    public void SaveProgress(string key, Save saveData)
    {
        var json = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString(key, json);
    }

    public Save LoadProgress(string key)
    {
        var json = PlayerPrefs.GetString(key);
        
        if (PlayerPrefs.HasKey(key))
        {
            return JsonUtility.FromJson<Save>(json);
        }
        else
        {
            Save save = new Save();
            SaveProgress(key, save);
            return save;
        }
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