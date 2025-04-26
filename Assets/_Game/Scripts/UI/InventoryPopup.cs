using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPopup : MonoBehaviour
{
    [SerializeField] private GameObject boardInventory, imageFade;
    private bool lastIsInventory;

    private void Start()
    {
        lastIsInventory = false;
        boardInventory.SetActive(false);
        imageFade.SetActive(false);
    }

    void Update()
    {
        if (HomeScene.instance != null && HomeScene.instance.isInventory == true && lastIsInventory == false)
        {
            PlayOpenTween();
            lastIsInventory = true;
        }

        if (HomeScene.instance != null && HomeScene.instance.isInventory == false && lastIsInventory == true)
        {
            PlayCloseTween();
            lastIsInventory = false;
        }
    }

    private void PlayOpenTween()
    {
        boardInventory.SetActive(true);
        imageFade.SetActive(true);

        boardInventory.transform.DOKill();
        boardInventory.transform.localScale = Vector3.zero;
        boardInventory.transform
            .DOScale(1f, 0.5f)
            .SetEase(Ease.OutBack);
    }
    private void PlayCloseTween()
    {
        boardInventory.transform.DOKill();

        boardInventory.transform
            .DOScale(0f, 0.5f)
            .SetEase(Ease.InBack)
            .OnComplete(() => {
                boardInventory.SetActive(false);
                imageFade.SetActive(false);
            });
    }
}
