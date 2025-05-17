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
    public GameObject endTransitionScene, iconLoading, btnNewGame;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("BtnNewGame") == false)
            PlayerPrefs.SetInt("BtnNewGame", 0);

        if (PlayerPrefs.GetInt("BtnNewGame") == 0)
            btnNewGame.SetActive(false);
        else
            btnNewGame.SetActive(true);
        
        isSetting = false;
        isShopping = false;
        isPlaying = false;

        endTransitionScene.SetActive(false);
        iconLoading.SetActive(false);
    }

    public void ButtonNewGame()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.button);
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        StartCoroutine(DelayNewGame("BookScene"));
    }

    public void ButtonPlay()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.button);
        StartCoroutine(DelayEndTransition("LevelSelect"));
    }

    public void ButtonBook()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.button);
        PlayerPrefs.DeleteKey("IsFirstTime");

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
    }

    IEnumerator DelayEndTransition(string nameScene)
    {
        endTransitionScene.SetActive(true);
        iconLoading.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadSceneAsync(nameScene);
    }

    IEnumerator DelayNewGame(string nameScene)
    {
        endTransitionScene.SetActive(true);
        iconLoading.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadSceneAsync(nameScene);

        yield return new WaitForSeconds(1.5f);
        PlayerPrefs.SetInt("BtnNewGame", 0);
    }
}
