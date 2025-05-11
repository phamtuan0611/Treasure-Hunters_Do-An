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
    public GameObject endTransitionScene;

    private void Start()
    {
        isLevelPopup = false;
        endTransitionScene.SetActive(false);
    }

    public void CloseLevelPopup()
    {
        isLevelPopup = false;
    }

    public void LoadHomeScene()
    {
        StartCoroutine(DelayEndTransition("HomeScene"));
    }

    public void LoadLevel()
    {
        StartCoroutine(DelayEndTransition(LevelPopup.instance.textBoard.text));
    }

    IEnumerator DelayEndTransition(string nameScene)
    {
        endTransitionScene.SetActive(true);

        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadSceneAsync(nameScene);
        //endTransitionScene.SetActive(false);
    }
}
