using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Victory : MonoBehaviour
{
    [SerializeField] private GameObject board, imageFirst;
    // Start is called before the first frame update
    public GameObject endTransitionScene;
    void Start()
    {
        imageFirst.SetActive(true);
        endTransitionScene.SetActive(false);

        RectTransform rt = board.GetComponent<RectTransform>();
        rt.DOAnchorPosY(0f, 10f).SetEase(Ease.OutCubic)
            .OnComplete(() =>
            {
                imageFirst.SetActive(false);
            });
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Home()
    {
        StartCoroutine(DelayEndTransition("HomeScene"));
    }

    IEnumerator DelayEndTransition(string nameScene)
    {
        endTransitionScene.SetActive(true);

        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadSceneAsync(nameScene);
        //endTransitionScene.SetActive(false);
    }
}
