using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelPopup : MonoBehaviour
{
    public static LevelPopup instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    [SerializeField] private GameObject boardLevel, imageFade;
    private bool lastIsLevel;
    public TMP_Text textBoard;

    private void Start()
    {
        lastIsLevel = false;
        boardLevel.SetActive(false);
        imageFade.SetActive(false);
    }

    void Update()
    {
        if (LevelSelect.instance != null && LevelSelect.instance.isLevelPopup == true && lastIsLevel == false)
        {
            PlayOpenTween();
            lastIsLevel = true;
        }

        if (LevelSelect.instance != null && LevelSelect.instance.isLevelPopup == false && lastIsLevel == true)
        {
            PlayCloseTween();
            lastIsLevel = false;
        }
    }

    private void PlayOpenTween()
    {
        boardLevel.SetActive(true);
        imageFade.SetActive(true);

        boardLevel.transform.DOKill();
        boardLevel.transform.localScale = Vector3.zero;
        boardLevel.transform
            .DOScale(1f, 0.5f)
            .SetEase(Ease.OutBack);
    }
    private void PlayCloseTween()
    {
        boardLevel.transform.DOKill();

        boardLevel.transform
            .DOScale(0f, 0.5f)
            .SetEase(Ease.InBack)
            .OnComplete(() => {
                boardLevel.SetActive(false);
                imageFade.SetActive(false);
            });
    }
}
