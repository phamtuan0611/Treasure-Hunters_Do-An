using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinPopup : MonoBehaviour
{
    [SerializeField] private GameObject boardWin, imageFade;
    private bool isWin;
    [SerializeField] private TMP_Text diamondText, fruitText;

    private void Start()
    {
        isWin = false;
        imageFade.SetActive(false);
        boardWin.SetActive(false);
    }

    void Update()
    {
        if (CheckEnd.instance != null && CheckEnd.instance.isWin == true && isWin == false)
        {
            isWin = true;
            diamondText.text = UIController.instance.diamondText.text;
            fruitText.text = UIController.instance.fruitText.text;
            StartCoroutine(DelayLost());
        }
    }

    private void PlayOpenTween()
    {
        boardWin.SetActive(true);
        imageFade.SetActive(true);

        boardWin.transform.DOKill();
        boardWin.transform.localScale = Vector3.zero;
        boardWin.transform
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
