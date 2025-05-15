using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public BoosterDatabase database;
    public int gemAmount;
    public int fruitAmount;

    public TMP_Text gemText;
    public TMP_Text fruitText;

    void Start()
    {
        LoadCurrency();
        UpdateCurrencyUI();
    }

    public void TryBuy(string boosterId, bool usingGem)
    {
        BoosterData booster = database.GetBooster(boosterId);

        if (booster == null) return;

        if (booster.currentAmount >= booster.maxAmount)
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.btnCantBuy);
            Debug.Log("Reached max limit");
            return;
        }

        int price = usingGem ? booster.priceGem : booster.priceFruit;

        if ((usingGem && gemAmount >= price) || (!usingGem && fruitAmount >= price))
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.btnBuyDone);
            if (usingGem)
                gemAmount -= price;
            else
                fruitAmount -= price;

            booster.currentAmount++;
            UpdateCurrencyUI();
            SaveAll();
        }
        else
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.btnCantBuy);
            Debug.Log("Not enough currency");
        }
    }
    public void BuyWithGem()
    {
        string boosterId = "life"; 
        TryBuy(boosterId, true);
    }

    public void BuyWithFruit()
    {
        string boosterId = "life"; 
        TryBuy(boosterId, false); 
    }

    void UpdateCurrencyUI()
    {
        gemText.text = gemAmount.ToString();
        fruitText.text = fruitAmount.ToString();
    }

    public void SaveAll()
    {
        PlayerPrefs.SetInt("gem", gemAmount);
        PlayerPrefs.SetInt("fruit", fruitAmount);

        BoosterSaveWrapper wrapper = new BoosterSaveWrapper { boosters = database.boosters };
        string json = JsonUtility.ToJson(wrapper);
        PlayerPrefs.SetString("booster_save", json);

        PlayerPrefs.Save();
    }

    void LoadCurrency()
    {
        gemAmount = PlayerPrefs.GetInt("gem", 500);
        fruitAmount = PlayerPrefs.GetInt("fruit", 500);
    }
}
