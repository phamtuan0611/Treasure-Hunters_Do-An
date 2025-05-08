using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckEnd : MonoBehaviour
{
    public static CheckEnd instance;
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

    private void Start()
    {
        isWin = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetTrigger("isPlayer");
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
            SceneManager.LoadScene(nameScene);
        }
    }
}
