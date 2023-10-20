using System;
using UnityEngine;
using UnityEngine.Serialization;

public class ProgressHandler
{
    public void SaveProgress(string key, Save saveData)
    {
        var json = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString(key, json);
    }

    public Save LoadProgress(string key)
    {
        var json = PlayerPrefs.GetString(key);
        return JsonUtility.FromJson<Save>(json);
    }

    [Serializable]
    public struct Save
    {
        public int Level;
        public int Price;
        public int Wallet;
        public int CurrentIndex;
    }
}

