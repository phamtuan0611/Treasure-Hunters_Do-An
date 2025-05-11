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
    public GameObject endTransitionScene;
    // Start is called before the first frame update
    void Start()
    {
        isSetting = false;
        isShopping = false;
        isPlaying = false;

        endTransitionScene.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ButtonPlay()
    {
        //isPlaying = true;
        StartCoroutine(DelayEndTransition("LevelSelect"));
    }

    public void ButtonShopOpen()
    {
        isShopping = true;
    }
    public void ButtonShopClose()
    {
        isShopping = false;
    }
    public void ButtonSettingOpen()
    {
        isSetting = true;
    }
    public void ButtonSettingClose()
    {
        isSetting = false;
    }
    public void ButtonQuit()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }

    IEnumerator DelayEndTransition(string nameScene)
    {
        endTransitionScene.SetActive(true);

        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadSceneAsync(nameScene);
        //endTransitionScene.SetActive(false);
    }
}
