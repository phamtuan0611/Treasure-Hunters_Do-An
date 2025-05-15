using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private GameObject boardPotionBonus;

    [SerializeField] private GameObject[] iconPotion;
    private bool isOpen = true;

    private Vector3 originalPosition;
    // Start is called before the first frame update
    void Start()
    {
        iconPotion[0].SetActive(isOpen);
        iconPotion[1].SetActive(!isOpen);

        originalPosition = boardPotionBonus.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ButtonPotionBoard()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.button);

        isOpen = !isOpen;

        iconPotion[0].SetActive(isOpen);
        iconPotion[1].SetActive(!isOpen);

        Vector3 targetPosition = isOpen
            ? originalPosition
            : originalPosition + new Vector3(0, 250f, 0);

        boardPotionBonus.transform.DOLocalMove(targetPosition, 0.5f).SetEase(Ease.OutQuad);
    }
}
