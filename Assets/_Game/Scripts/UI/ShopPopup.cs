using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopPopup : MonoBehaviour
{
    [SerializeField] private GameObject boardShop, imageFade, buyPopup;
    private bool lastIsShop, buyInShop;

    [SerializeField] private GameObject[] imageCoin;

    [SerializeField] private UnityEngine.UI.Image topIconDiamond;
    [SerializeField] private TMP_Text descriptionTextDiamond;
    [SerializeField] private UnityEngine.UI.Image topIconFruit;
    [SerializeField] private TMP_Text descriptionTextFruit;

    [SerializeField] private UnityEngine.UI.Button btn1;
    [SerializeField] private TMP_Text btn1Text;

    [SerializeField] private UnityEngine.UI.Button btn2;
    [SerializeField] private TMP_Text btn2Text;

    private void Start()
    {
        lastIsShop = false;
        buyInShop = false;

        boardShop.SetActive(false);
        imageFade.SetActive(false);
        buyPopup.SetActive(false);
    }

    void Update()
    {
        if (HomeScene.instance != null && HomeScene.instance.isShopping == true && lastIsShop == false)
        {
            PlayOpenTween(boardShop);
            imageFade.SetActive(true);

            lastIsShop = true;
        }

        if (HomeScene.instance != null && HomeScene.instance.isShopping == false && lastIsShop == true)
        {
            PlayCloseTween();
            lastIsShop = false;
        }

        if (ShopController.instance != null && ShopController.instance.isBuying == true && buyInShop == false)
        {
            PlayOpenTween(buyPopup);
            buyInShop = true;
        }
        
        if (ShopController.instance != null && ShopController.instance.isBuying == false && buyInShop == true)
        {
            PlayCloseTweenBuy();
            buyInShop = false;
        }
    }

    public void SetupPopup(Sprite iconDiamond, string descDiamond, Sprite iconFruit, string descFruit,
                        string btn1Str, string btn2Str)
    {
        topIconDiamond.sprite = iconDiamond;
        descriptionTextDiamond.text = descDiamond;
        
        topIconFruit.sprite = iconFruit;
        descriptionTextFruit.text = descFruit;

        btn1Text.text = btn1Str;

        btn2Text.text = btn2Str;
    }

    private void PlayOpenTween(GameObject popup)
    {
        popup.SetActive(true);

        foreach (GameObject item in imageCoin) 
        {
            item.transform.SetParent(transform);
        }

        popup.transform.DOKill();
        popup.transform.localScale = Vector3.zero;
        popup.transform
            .DOScale(1f, 0.5f)
            .SetEase(Ease.OutBack);
    }
    private void PlayCloseTween()
    {
        boardShop.transform.DOKill();

        boardShop.transform
            .DOScale(0f, 0.5f)
            .SetEase(Ease.InBack)
            .OnComplete(() => {
                boardShop.SetActive(false);
                imageFade.SetActive(false);
                foreach (GameObject item in imageCoin)
                {
                    item.transform.SetParent(HomeScene.instance.coins.transform);
                }
            });
    }

    private void PlayCloseTweenBuy()
    {
        buyPopup.transform.DOKill();

        buyPopup.transform
            .DOScale(0f, 0.5f)
            .SetEase(Ease.InBack);
    }
}
