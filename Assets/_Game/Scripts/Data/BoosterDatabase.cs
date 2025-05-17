using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterDatabase : MonoBehaviour
{
    public static BoosterDatabase instance;

    public List<BoosterData> boosters = new List<BoosterData>();

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        if (!PlayerPrefs.HasKey("booster_save"))
        {
            boosters = new List<BoosterData>()
            {
                new BoosterData { id = "life", displayName = "Life", currentAmount = 0, maxAmount = 3, priceGem = 100, priceFruit = 150 },
                new BoosterData { id = "shield", displayName = "Shield", currentAmount = 0, maxAmount = 3, priceGem = 100, priceFruit = 150 },
                new BoosterData { id = "speed", displayName = "x2 Speed", currentAmount = 0, maxAmount = 3, priceGem = 50, priceFruit = 100 },
                new BoosterData { id = "pickup", displayName = "x2 Pickup", currentAmount = 0, maxAmount = 3, priceGem = 150, priceFruit = 250 },
                new BoosterData { id = "jump", displayName = "x1.3 Jump", currentAmount = 0, maxAmount = 3, priceGem = 50, priceFruit = 100 },
            };
            SaveSystem.SaveBoosters(boosters);
        }
        else
        {
            boosters = SaveSystem.LoadBoosters();
        }
    }

    public BoosterData GetBooster(string id)
    {
        return boosters.Find(b => b.id == id);
    }
}
