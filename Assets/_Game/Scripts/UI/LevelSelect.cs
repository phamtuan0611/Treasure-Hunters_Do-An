using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public static LevelSelect instance;
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

    public bool isLevelPopup;
    public GameObject endTransitionScene, iconLoading;

    private void Start()
    {
        isLevelPopup = false;
        endTransitionScene.SetActive(false); iconLoading.SetActive(false);
    }

    public void CloseLevelPopup()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.button);
        isLevelPopup = false;
    }

    public void LoadHomeScene()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.button);
        StartCoroutine(DelayEndTransition("HomeScene"));
    }

    public void LoadLevel()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.button);
        StartCoroutine(DelayEndTransition(LevelPopup.instance.textBoard.text));
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
