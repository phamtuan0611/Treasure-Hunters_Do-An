using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BookScene : MonoBehaviour
{
    public GameObject endTransitionScene, iconLoading, btnSkip;
    public float growBtnSkip;
    [SerializeField] private GameObject waitScreen;
    private CanvasGroup waitCanvasGroup;
    // Start is called before the first frame update
    void Start()
    {
        endTransitionScene.SetActive(false);
        iconLoading.SetActive(false);

        waitCanvasGroup = waitScreen.GetComponent<CanvasGroup>();
        waitCanvasGroup.alpha = 0f;
        waitScreen.SetActive(false);
        StartCoroutine(WaitScreen());

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
        AudioManager.instance.PlaySFX(AudioManager.instance.button);
        StartCoroutine(DelayEndTransition("HomeScene"));
    }
    IEnumerator DelayEndTransition(string nameScene)
    {
        endTransitionScene.SetActive(true);
        iconLoading.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadSceneAsync(nameScene);
    }

    IEnumerator WaitScreen()
    {
        waitScreen.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.9f);

        waitScreen.SetActive(true);

        float duration = 1f;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            waitCanvasGroup.alpha = Mathf.Lerp(0f, 1f, time / duration);
            yield return null;
        }
        waitCanvasGroup.alpha = 1f;

        yield return new WaitForSeconds(3f);

        // Fade out
        time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            waitCanvasGroup.alpha = Mathf.Lerp(1f, 0f, time / duration);
            yield return null;
        }

        waitCanvasGroup.alpha = 0f;
        waitScreen.SetActive(false);
    }
}
