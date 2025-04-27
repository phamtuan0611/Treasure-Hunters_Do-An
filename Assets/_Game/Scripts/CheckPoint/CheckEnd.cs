using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckEnd : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private string nameScene;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetTrigger("isPlayer");
            StartCoroutine(DelayLoadScene());
        }
    }

    IEnumerator DelayLoadScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(nameScene);
    }
}
