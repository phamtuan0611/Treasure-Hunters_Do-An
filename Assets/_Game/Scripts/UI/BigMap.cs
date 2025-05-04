using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigMap : MonoBehaviour
{
    [SerializeField] private GameObject bigMap, imageFade;
    private bool lastIsBigMap;

    private void Start()
    {
        lastIsBigMap = false;
        bigMap.SetActive(false);
        imageFade.SetActive(false);
    }

    void Update()
    {
        if (UIController.instance != null && UIController.instance.isBigMap == true && lastIsBigMap == false)
        {
            PlayOpenTween();
            lastIsBigMap = true;
        }

        if (UIController.instance != null && UIController.instance.isBigMap == false && lastIsBigMap == true)
        {
            PlayCloseTween();
            lastIsBigMap = false;
        }
    }

    private void PlayOpenTween()
    {
        bigMap.SetActive(true);
        imageFade.SetActive(true);

        bigMap.transform.DOKill();
        bigMap.transform.localScale = Vector3.zero;
        bigMap.transform
            .DOScale(1f, 0.5f)
            .SetEase(Ease.OutBack);
    }
    private void PlayCloseTween()
    {
        bigMap.transform.DOKill();

        bigMap.transform
            .DOScale(0f, 0.5f)
            .SetEase(Ease.InBack)
            .OnComplete(() => {
                bigMap.SetActive(false);
                imageFade.SetActive(false);
            });
    }
}
