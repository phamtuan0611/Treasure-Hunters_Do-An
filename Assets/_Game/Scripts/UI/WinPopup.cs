using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinPopup : MonoBehaviour
{
    [SerializeField] private GameObject boardWin, imageFade, coinUI;
    private bool isWin;
    [SerializeField] private TMP_Text diamondText, fruitText;
    [SerializeField] private TMP_Text diamondAll, fruitAll;

    private void Start()
    {
        isWin = false;
        imageFade.SetActive(false);
        boardWin.SetActive(false);
        coinUI.SetActive(false);
    }

    void Update()
    {
        if (CheckEnd.instance != null && CheckEnd.instance.isWin == true && isWin == false)
        {
            isWin = true;
            diamondText.text = UIController.instance.diamondText.text;
            fruitText.text = UIController.instance.fruitText.text;

            var (currentGem, currentFruit) = SaveSystem.LoadCurrency();

            int diamondEarned = int.TryParse(diamondText.text, out int d) ? d : 0;
            int fruitEarned = int.TryParse(fruitText.text, out int f) ? f : 0;

            SaveSystem.SaveCurrency(currentGem + diamondEarned, currentFruit + fruitEarned);

            diamondAll.text = currentGem.ToString();
            fruitAll.text = currentFruit.ToString();

            int newGem = currentGem + diamondEarned;
            int newFruit = currentFruit + fruitEarned;

            SaveSystem.SaveCurrency(newGem, newFruit);

            StartCoroutine(AnimateNumber(diamondAll, currentGem, newGem, 2f));
            StartCoroutine(AnimateNumber(fruitAll, currentFruit, newFruit, 2f));

            StartCoroutine(DelayWin());
        }
    }

    IEnumerator AnimateNumber(TMP_Text textUI, int from, int to, float duration)
    {
        yield return new WaitForSeconds(1.8f);

        DOVirtual.Int(from, to, duration, value =>
        {
            textUI.text = value.ToString();
        });
    }

    private void PlayOpenTween()
    {
        boardWin.SetActive(true);
        imageFade.SetActive(true);
        coinUI.SetActive(true);

        boardWin.transform.DOKill();
        boardWin.transform.localScale = Vector3.zero;
        boardWin.transform
            .DOScale(1f, 0.5f)
            .SetEase(Ease.OutBack)
            .SetUpdate(true);
    }

    IEnumerator DelayWin()
    {
        yield return new WaitForSeconds(1.5f);
        PlayOpenTween();
    }
}
