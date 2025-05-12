using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject startSceneTransition;

    private void Start()
    {
        startSceneTransition.SetActive(true);
        StartCoroutine(DisableStartScene());
    }

    IEnumerator DisableStartScene()
    {
        yield return new WaitForSeconds(1.5f);
        startSceneTransition.SetActive(false);
    }
}
