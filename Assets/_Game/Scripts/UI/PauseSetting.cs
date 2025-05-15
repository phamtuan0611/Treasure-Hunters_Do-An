using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseSetting : MonoBehaviour
{
    [SerializeField] private GameObject[] iconMusic, iconSFX;
    private bool isMusicOn = true;
    private bool isSFXOn = true;
    // Start is called before the first frame update
    void Start()
    {
        isMusicOn = AudioManager.instance.GetMusicStatus();
        isSFXOn = AudioManager.instance.GetSFXStatus();

        iconMusic[0].SetActive(isMusicOn);
        iconMusic[1].SetActive(!isMusicOn);

        iconSFX[0].SetActive(isSFXOn);
        iconSFX[1].SetActive(!isSFXOn);
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
