using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPopup : MonoBehaviour
{
    [SerializeField] private GameObject boardShop, imageFade;
    private bool lastIsShop;

    [SerializeField] private GameObject[] imageCoin;

    private void Start()
    {
        lastIsShop = false;
        boardShop.SetActive(false);
        imageFade.SetActive(false);
    }

    void Update()
    {
        if (HomeScene.instance != null && HomeScene.instance.isShopping == true && lastIsShop == false)
        {
            PlayOpenTween();
            lastIsShop = true;
        }

        if (HomeScene.instance != null && HomeScene.instance.isShopping == false && lastIsShop == true)
        {
            PlayCloseTween();
            lastIsShop = false;
        }
    }

    private void PlayOpenTween()
    {
        boardShop.SetActive(true);
        imageFade.SetActive(true);

        foreach (GameObject item in imageCoin) 
        {
            item.transform.SetParent(transform);
        }

        boardShop.transform.DOKill();
        boardShop.transform.localScale = Vector3.zero;
        boardShop.transform
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
}
