using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckEnd : MonoBehaviour
{
    public static CheckEnd instance;
    public int levelIndex;
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

    [SerializeField] private Animator anim;
    [SerializeField] private string nameScene;
    public bool isWin;

    public GameObject endTransitionScene, iconLoading;
    private void Start()
    {
        isWin = false;

        endTransitionScene.SetActive(false);
        iconLoading.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            int completed = PlayerPrefs.GetInt("CompletedLevel");
            if (levelIndex > completed)
            {
                PlayerPrefs.SetInt("CompletedLevel", levelIndex);
                PlayerPrefs.Save();
            }

            anim.SetTrigger("isPlayer");
            AudioManager.instance.PlaySFX(AudioManager.instance.checkEnd);
            StartCoroutine(DelayWin());
        }
    }

    IEnumerator DelayWin()
    {
        yield return new WaitForSeconds(2f);
        isWin = true;
    }

    public void LoadNextLevel()
    {
        if (isWin)
        {
            //SceneManager.LoadSceneAsync(nameScene);
            StartCoroutine(DelayEndTransition(nameScene));
        }
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
