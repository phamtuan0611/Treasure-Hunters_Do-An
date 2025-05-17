using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Victory : MonoBehaviour
{
    [SerializeField] private GameObject board, imageFirst;
    // Start is called before the first frame update
    public GameObject endTransitionScene, iconLoading;
    void Start()
    {
        imageFirst.SetActive(true);
        endTransitionScene.SetActive(false);
        iconLoading.SetActive(false);

        RectTransform rt = board.GetComponent<RectTransform>();
        rt.DOAnchorPosY(0f, 10f).SetEase(Ease.OutCubic)
            .OnComplete(() =>
            {
                imageFirst.SetActive(false);
            });
    }

    public void QuitGame()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.button);
        Application.Quit();
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
}
