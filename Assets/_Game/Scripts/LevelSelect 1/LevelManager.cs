using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

[Serializable]
public class LevelData
{
    public string nameLevel;
    public GameObject bridge, smallMap;
    public GameObject close, done;
    public GameObject blockImage;
}

public class LevelManager : MonoBehaviour
{


    public List<LevelData> levelDatas = new List<LevelData>();
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("CompletedLevel") == false)
        {
            PlayerPrefs.SetInt("CompletedLevel", -1);
        }

        int completedLevel = PlayerPrefs.GetInt("CompletedLevel");
        
        if (completedLevel < 0)
            ApplyLevelStates();
        else 
            OnCompleteLevel(completedLevel);

    }

    void ApplyLevelStates()
    {
        foreach (var level in levelDatas)
        {
            if (level.bridge != null)
                level.bridge.SetActive(false);

            if (level.smallMap != null)
                level.smallMap.SetActive(true);

            if (level.close != null)
                level.close.SetActive(true);

            if (level.done != null)
                level.done.SetActive(false);

            if (level.blockImage != null)
                level.blockImage.SetActive(true);
        }
    }
    public void OnCompleteLevel(int levelIndex)
    {
        ApplyLevelStates();
        
        for (int i = 0; i <= levelIndex && i < levelDatas.Count; i++)
        {
            LevelData level = levelDatas[i];

            if (level.bridge != null)
                level.bridge.SetActive(true);

            if (level.smallMap != null)
                level.smallMap.SetActive(false);

            if (level.close != null)
                level.close.SetActive(false);

            if (level.done != null)
                level.done.SetActive(true);

            if (level.blockImage != null)
                level.blockImage.SetActive(false);
        }
    }
}
