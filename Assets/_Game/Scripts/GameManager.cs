using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject startSceneTransition;
    [SerializeField] private GameObject endSceneTransition;

    [SerializeField] private string loadSceneName;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "HomeScene")
        {
            return;
        }

        startSceneTransition.SetActive(true);
        StartCoroutine(DisableStartScene());
    }

    private void Update()
    {
        if (HomeScene.instance != null && HomeScene.instance.isPlaying == true)
        {
            endSceneTransition.SetActive(true);
            HomeScene.instance.isPlaying = false;

            StartCoroutine(DisableEndScene());
        }
    }

    IEnumerator DisableStartScene()
    {
        yield return new WaitForSeconds(1.5f);
        startSceneTransition.SetActive(false);
    }

    IEnumerator DisableEndScene()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(loadSceneName);

    }
}
