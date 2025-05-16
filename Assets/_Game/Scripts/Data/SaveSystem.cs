using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveSystem
{
    public static void SaveCurrency(int gem, int fruit)
    {
        PlayerPrefs.SetInt("gem", gem);
        PlayerPrefs.SetInt("fruit", fruit);
    }

    public static (int gem, int fruit) LoadCurrency()
    {
        int gem = PlayerPrefs.GetInt("gem", 1000);
        int fruit = PlayerPrefs.GetInt("fruit", 1000);
        return (gem, fruit);
    }

    public static void SaveBoosters(List<BoosterData> boosters)
    {
        string json = JsonUtility.ToJson(new BoosterSaveWrapper { boosters = boosters });
        PlayerPrefs.SetString("booster_save", json);
    }

    public static List<BoosterData> LoadBoosters()
    {
        string json = PlayerPrefs.GetString("booster_save", "{}");
        BoosterSaveWrapper wrapper = JsonUtility.FromJson<BoosterSaveWrapper>(json);
        return wrapper != null && wrapper.boosters != null ? wrapper.boosters : new List<BoosterData>();
    }
}
