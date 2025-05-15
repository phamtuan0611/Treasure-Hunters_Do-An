using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostPopup : MonoBehaviour
{
    [SerializeField] private GameObject boardLost, imageFade;
    private bool isLost;

    private void Start()
    {
        isLost = false;
        imageFade.SetActive(false);
        boardLost.SetActive(false);
    }

    void Update()
    {
        if (LifeController.instance != null && LifeController.instance.currentLive == 0 && isLost == false)
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.loseLevel);
            isLost = true;
            StartCoroutine(DelayLost());
        }
    }

    private void PlayOpenTween()
    {
        boardLost.SetActive(true);
        imageFade.SetActive(true);

        boardLost.transform.DOKill();
        boardLost.transform.localScale = Vector3.zero;
        boardLost.transform
            .DOScale(1f, 0.5f)
            .SetEase(Ease.OutBack)
            .SetUpdate(true);
    }

    IEnumerator DelayLost()
    {
        yield return new WaitForSeconds(1.5f);
        PlayOpenTween();
    }
}
