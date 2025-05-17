using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UIController : MonoBehaviour
{
    public static UIController instance;

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

    [SerializeField] private Image[] heartIcons, skullGates;
    [SerializeField] private Sprite healthFull, healthEmpty;
    [SerializeField] public TMP_Text liveText, fruitText, diamondText;

    public GameObject highSpeed, lowSpeed, multiplyScore;
    [SerializeField] private TMP_Text highSpeedText, lowSpeedText, multiplyScoreText;

    [SerializeField] private GameObject waitScreen;
    private CanvasGroup waitCanvasGroup;

    public bool isBigMap, isPause;

    public GameObject endTransitionScene, iconLoading;

    // Start is called before the first frame update
    void Start()
    {
        highSpeed.SetActive(false);
        lowSpeed.SetActive(false);
        multiplyScore.SetActive(false);

        waitCanvasGroup = waitScreen.GetComponent<CanvasGroup>();
        waitCanvasGroup.alpha = 0f;
        waitScreen.SetActive(false);
        StartCoroutine(WaitScreen());

        isBigMap = false;
        isPause = false;

        endTransitionScene.SetActive(false); iconLoading.SetActive(false);
    }

    public void UpdateHealthDisplay(int health, int maxHealth)
    {
        for (int i = 0; i < heartIcons.Length; i++)
        {
            heartIcons[i].enabled = true;

            if (health > i)
            {
                heartIcons[i].sprite = healthFull;
            }
            else
            {
                heartIcons[i].sprite = healthEmpty;
                if (maxHealth <= i)
                {
                    heartIcons[i].enabled = false;
                }
            }
        }
    }

    public void UpdateSkullGate(int amount)
    {
        for (int i = 0; i < skullGates.Length; i++)
        {
            skullGates[i].color = new Color(1, 1, 1, 1);

            if (i >= amount - 1)
                return;
        }
    }

    public void UpdateLiveDisplay(int currentLive)
    {
        liveText.text = currentLive.ToString();
    }

    public void UpdateFruitDisplay(int currentFruit)
    {
        fruitText.text = currentFruit.ToString();
    }

    public void UpdateDiamondDisplay(int currentDiamond)
    {
        diamondText.text = currentDiamond.ToString();
    }

    public void UpdateHighSpeed(float timeHighSpeed)
    {
        highSpeed.SetActive(true);
        highSpeedText.text = Mathf.CeilToInt(timeHighSpeed).ToString();
    }

    public void UpdateLowSpeed(float timeLowSpeed)
    {
        lowSpeed.SetActive(true);
        lowSpeedText.text = Mathf.CeilToInt(timeLowSpeed).ToString();
    }

    public void UpdateMultiplyScore(float timeMultiplyScore)
    {
        multiplyScore.SetActive(true);
        multiplyScoreText.text = Mathf.CeilToInt(timeMultiplyScore).ToString();
    }
    public void LoadLevelScene()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.button);
        Time.timeScale = 1f;
        StartCoroutine(DelayEndTransition("LevelSelect"));
    }

    public void OpenBigMap()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.button);
        isBigMap = true;
    }

    public void CloseBigMap()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.button);
        isBigMap = false;
    }

    public void OpenPausePopup()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.button);
        isPause = true;
        Time.timeScale = 0f;
    }

    public void ClosePausePopup()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.button);
        isPause = false;
        Time.timeScale = 1f;
    }

    public void ReStartLevel()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.button);
        Time.timeScale = 1f;
        StartCoroutine(DelayEndTransition(SceneManager.GetActiveScene().name));
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

        yield return new WaitForSeconds(1.5f);

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
