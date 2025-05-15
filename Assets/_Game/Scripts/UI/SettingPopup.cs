using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SettingPopup : MonoBehaviour
{
    [SerializeField] private GameObject boardSetting, imageFade;
    private bool lastIsSetting;

    [SerializeField] private GameObject[] iconMusic, iconSFX;
    private bool isMusicOn = true;
    private bool isSFXOn = true;

    private void Start()
    {
        lastIsSetting = false;
        boardSetting.SetActive(false);
        imageFade.SetActive(false);

        isMusicOn = AudioManager.instance.GetMusicStatus();
        isSFXOn = AudioManager.instance.GetSFXStatus();

        iconMusic[0].SetActive(isMusicOn);
        iconMusic[1].SetActive(!isMusicOn);

        iconSFX[0].SetActive(isSFXOn);
        iconSFX[1].SetActive(!isSFXOn);
    }

    void Update()
    {
        if (HomeScene.instance != null && HomeScene.instance.isSetting == true && lastIsSetting == false)
        {
            PlayOpenTween();
            lastIsSetting = true;
        }

        if (HomeScene.instance != null && HomeScene.instance.isSetting == false && lastIsSetting == true)
        {
            PlayCloseTween();
            lastIsSetting = false;
        }
    }

    private void PlayOpenTween()
    {
        boardSetting.SetActive(true);
        imageFade.SetActive(true);

        boardSetting.transform.DOKill();
        boardSetting.transform.localScale = Vector3.zero;
        boardSetting.transform
            .DOScale(1f, 0.5f)
            .SetEase(Ease.OutBack);
    }
    private void PlayCloseTween()
    {
        boardSetting.transform.DOKill();

        boardSetting.transform
            .DOScale(0f, 0.5f)
            .SetEase(Ease.InBack) 
            .OnComplete(() => {
                boardSetting.SetActive(false);     
                imageFade.SetActive(false);
            });
    }

    public void ButtonMusic()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.button);
        isMusicOn = !isMusicOn;

        AudioManager.instance.SetMusic(isMusicOn);

        iconMusic[0].SetActive(isMusicOn);   
        iconMusic[1].SetActive(!isMusicOn);  
    }

    public void ButtonSFX()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.button);
        isSFXOn = !isSFXOn; 
        
        AudioManager.instance.SetSFX(isSFXOn);

        iconSFX[0].SetActive(isSFXOn);       
        iconSFX[1].SetActive(!isSFXOn);      
    }
}
