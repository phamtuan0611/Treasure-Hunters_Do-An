using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class BoosterPotionUI
{
    public string boosterPotinId;
    public TMP_Text amountPotinText;
}
public class BoardPotion : MonoBehaviour
{
    public BoosterDatabase boosterDatabase;
    public List<BoosterPotionUI> boosterPotionUIs;

    public bool isPotion;
    // Start is called before the first frame update
    void Start()
    {
        if (boosterDatabase == null)
            boosterDatabase = new BoosterDatabase();
        boosterDatabase.boosters = SaveSystem.LoadBoosters();
        UpdateAllBoosterPotionUIs();

        isPotion = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UsePotion(string id)
    {
        BoosterData booster = boosterDatabase.GetBooster(id);
        if (booster == null) return;

        if (booster.currentAmount > 0)
        {
            if (booster.id == "life")
            {
                if (PlayerHealthController.instance.currentHealth == PlayerHealthController.instance.maxHealth)
                    return;
                else
                    booster.currentAmount--;
            }else
                booster.currentAmount--;
            
            SaveSystem.SaveBoosters(boosterDatabase.boosters);
            UpdateSingleBoosterPotionUI(id);

            isPotion = true;
        }
        else
            isPotion = false;
    }

    private void UpdateAllBoosterPotionUIs()
    {
        foreach (var item in boosterPotionUIs)
        {
            BoosterData booster = boosterDatabase.GetBooster(item.boosterPotinId);
            if (booster != null)
            {
                item.amountPotinText.text = $"{booster.currentAmount}";
            }
        }
    }

    private void UpdateSingleBoosterPotionUI(string boosterId)
    {
        foreach (var item in boosterPotionUIs)
        {
            if (item.boosterPotinId == boosterId)
            {
                BoosterData booster = boosterDatabase.GetBooster(boosterId);
                if (booster != null)
                {
                    item.amountPotinText.text = $"{booster.currentAmount}";
                }
                break;
            }
        }
    }
}
