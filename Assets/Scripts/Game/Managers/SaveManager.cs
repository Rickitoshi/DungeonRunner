using System;
using UnityEngine;
using Zenject;

public class SaveManager : IInitializable
{
    private const string SAVE_KEY = "SaveData";
    
    public SaveData Data { get; private set; }

    public void Initialize()
    {
        if (PlayerPrefs.HasKey(SAVE_KEY))
        {
            LoadData();
        }
        else
        {
            Data = new SaveData();
        }
    }

    public void SaveData()
    {
        var json = JsonUtility.ToJson(Data);
        PlayerPrefs.SetString(SAVE_KEY, json);
    }

    private void LoadData()
    {
        var data = PlayerPrefs.GetString(SAVE_KEY, String.Empty);
        Data = JsonUtility.FromJson<SaveData>(data);
    }
    
}

[Serializable]
public class SaveData
{
    public int Coins;
    public int[] LevelStatsIndex;

    public SaveData()
    {
        Coins = 0;
        LevelStatsIndex = new int[15];
    }
}
