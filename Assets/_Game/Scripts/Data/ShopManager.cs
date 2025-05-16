using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

[System.Serializable]
public class BoosterAmountUI
{
    public string boosterId;
    public TMP_Text amountText;
}
public class ShopManager : MonoBehaviour
{
    public BoosterDatabase boosterDatabase;
    public TMP_Text gemText;
    public TMP_Text fruitText;

    private int gemAmount;
    private int fruitAmount;

    private string currentSelectedBoosterId;

    public List<BoosterAmountUI> boosterAmountUIs;

    public GameObject textBuy;
    private void Start()
    {
        (gemAmount, fruitAmount) = SaveSystem.LoadCurrency();
        UpdateCurrencyUI();
        UpdateAllBoosterAmountUIs();
    }

    public void OpenBuyPopup(string boosterId) 
    {
        currentSelectedBoosterId = boosterId;
    }

    public void BuyWithGem(string boosterId)
    {
        if (!string.IsNullOrEmpty(currentSelectedBoosterId))
            TryBuy(currentSelectedBoosterId, true);
        else
            return;
    }

    public void BuyWithFruit(string boosterId)
    {
        if (!string.IsNullOrEmpty(currentSelectedBoosterId))
            TryBuy(currentSelectedBoosterId, false);
        else
            return;
    }

    private void TryBuy(string id, bool usingGem)
    {
        BoosterData booster = boosterDatabase.GetBooster(id);
        if (booster == null) return;

        int price = usingGem ? booster.priceGem : booster.priceFruit;
        int currency = usingGem ? gemAmount : fruitAmount;

        if (currency >= price && booster.currentAmount < booster.maxAmount)
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.btnBuyDone);

            if (usingGem)
                gemAmount -= price;
            else
                fruitAmount -= price;

            booster.currentAmount++;
            SaveSystem.SaveCurrency(gemAmount, fruitAmount);
            SaveSystem.SaveBoosters(boosterDatabase.boosters);
            UpdateCurrencyUI();

            UpdateSingleBoosterAmountUI(id);

            GameObject text = Instantiate(textBuy, ShopController.instance.transform.position, Quaternion.identity);
            text.transform.SetParent(ShopController.instance.transform);
            TextMeshProUGUI tmp = text.GetComponent<TextMeshProUGUI>();
            tmp.text = "+1 " + booster.displayName;
            tmp.alpha = 1f;

            text.transform.localScale = Vector3.zero;
            text.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
            text.transform.DOMoveY(text.transform.position.y + 1.25f, 1f).SetEase(Ease.OutSine);

            tmp.DOFade(0f, 0.5f).SetDelay(0.7f);
            Destroy(text, 1.3f);
        }
        else if (booster.currentAmount >= booster.maxAmount)
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.btnCantBuy);
            GameObject text = Instantiate(textBuy, ShopController.instance.transform.position, Quaternion.identity);
            text.transform.SetParent(ShopController.instance.transform);
            TextMeshProUGUI tmp = text.GetComponent<TextMeshProUGUI>();
            tmp.text = "MAX";
            tmp.color = Color.yellow;
            tmp.alpha = 1f;

            text.transform.localScale = Vector3.zero;
            text.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
            text.transform.DOMoveY(text.transform.position.y + 1.25f, 1f).SetEase(Ease.OutSine);

            tmp.DOFade(0f, 0.5f).SetDelay(0.7f);
            Destroy(text, 1.3f);

            return;
        }
        else
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.btnCantBuy);
            GameObject text = Instantiate(textBuy, ShopController.instance.transform.position, Quaternion.identity);
            text.transform.SetParent(ShopController.instance.transform);
            TextMeshProUGUI tmp = text.GetComponent<TextMeshProUGUI>();
            tmp.text = "Not enough!";
            tmp.color = Color.red;
            tmp.alpha = 1f;

            text.transform.localScale = Vector3.zero;
            text.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
            text.transform.DOMoveY(text.transform.position.y + 1.25f, 1f).SetEase(Ease.OutSine);

            tmp.DOFade(0f, 0.5f).SetDelay(0.7f);
            Destroy(text, 1.3f);

            return;
        }
    }

    private void UpdateCurrencyUI()
    {
        gemText.text = $"{gemAmount}";
        fruitText.text = $"{fruitAmount}";
    }

    private void UpdateAllBoosterAmountUIs()
    {
        foreach (var item in boosterAmountUIs)
        {
            BoosterData booster = boosterDatabase.GetBooster(item.boosterId);
            if (booster != null)
            {
                item.amountText.text = $"{booster.currentAmount}";
            }
        }
    }

    private void UpdateSingleBoosterAmountUI(string boosterId)
    {
        foreach (var item in boosterAmountUIs)
        {
            if (item.boosterId == boosterId)
            {
                BoosterData booster = boosterDatabase.GetBooster(boosterId);
                if (booster != null)
                {
                    item.amountText.text = $"{booster.currentAmount}";
                }
                break;
            }
        }
    }
}
