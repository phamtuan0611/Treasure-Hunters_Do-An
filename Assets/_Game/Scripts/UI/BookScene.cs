using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BookScene : MonoBehaviour
{
    public GameObject endTransitionScene, iconLoading, btnSkip;
    public float growBtnSkip;
    // Start is called before the first frame update
    void Start()
    {
        endTransitionScene.SetActive(false);
        iconLoading.SetActive(false);
        
        btnSkip.transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (AutoFlip.instance.count == 5)
        {
            btnSkip.transform.localScale = Vector3.MoveTowards(btnSkip.transform.localScale, Vector3.one, growBtnSkip * Time.deltaTime);
        }
    }
    public void Home()
    {
        StartCoroutine(DelayEndTransition("HomeScene"));
    }
    IEnumerator DelayEndTransition(string nameScene)
    {
        endTransitionScene.SetActive(true);
        iconLoading.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadSceneAsync(nameScene);
    }
}
