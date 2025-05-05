using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePopup : MonoBehaviour
{
    [SerializeField] private GameObject boardPause, imageFade;
    private bool lastIsPause;

    private void Start()
    {
        lastIsPause = false;
        boardPause.SetActive(false);
        imageFade.SetActive(false);
    }

    void Update()
    {
        if (UIController.instance != null && UIController.instance.isPause == true && lastIsPause == false)
        {
            PlayOpenTween();
            lastIsPause = true;
        }

        if (UIController.instance != null && UIController.instance.isPause == false && lastIsPause == true)
        {
            PlayCloseTween();
            lastIsPause = false;
        }
    }

    private void PlayOpenTween()
    {
        boardPause.SetActive(true);
        imageFade.SetActive(true);

        boardPause.transform.DOKill();
        boardPause.transform.localScale = Vector3.zero;
        boardPause.transform
            .DOScale(1f, 0.5f)
            .SetEase(Ease.OutBack)
            .SetUpdate(true);
    }
    private void PlayCloseTween()
    {
        boardPause.transform.DOKill();

        boardPause.transform
            .DOScale(0f, 0.5f)
            .SetEase(Ease.InBack)
            .SetUpdate(true)
            .OnComplete(() => {
                boardPause.SetActive(false);
                imageFade.SetActive(false);
            });
    }
}
