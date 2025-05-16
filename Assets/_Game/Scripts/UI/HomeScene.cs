using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeScene : MonoBehaviour
{
    public static HomeScene instance;
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

        DOTween.Init();

    }

    public bool isSetting, isShopping;
    public GameObject coins;

    public bool isPlaying;
    public GameObject endTransitionScene, iconLoading;
    // Start is called before the first frame update
    void Start()
    {
        isSetting = false;
        isShopping = false;
        isPlaying = false;

        endTransitionScene.SetActive(false);
        iconLoading.SetActive(false);
    }

    public void ButtonNewGame()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.button);
        //StartCoroutine(DelayEndTransition("BookScene"));
        PlayerPrefs.DeleteAll();
    }

    public void ButtonPlay()
    {
        //isPlaying = true;
        AudioManager.instance.PlaySFX(AudioManager.instance.button);
        StartCoroutine(DelayEndTransition("LevelSelect"));
    }

    public void ButtonBook()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.button);
        StartCoroutine(DelayEndTransition("BookScene"));
    }

    public void ButtonShopOpen()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.button);
        isShopping = true;
    }
    public void ButtonShopClose()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.button);
        isShopping = false;
    }
    public void ButtonSettingOpen()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.button);
        isSetting = true;
    }
    public void ButtonSettingClose()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.button);
        isSetting = false;
    }
    public void ButtonQuit()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.button);
        Application.Quit();
        Debug.Log("Quit Game");
    }

    IEnumerator DelayEndTransition(string nameScene)
    {
        endTransitionScene.SetActive(true);
        iconLoading.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadSceneAsync(nameScene);
        //endTransitionScene.SetActive(false);
    }
}
