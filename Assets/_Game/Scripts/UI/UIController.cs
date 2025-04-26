using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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
    [SerializeField] private TMP_Text liveText, fruitText, diamondText;

    public GameObject highSpeed, lowSpeed, multiplyScore;
    [SerializeField] private TMP_Text highSpeedText, lowSpeedText, multiplyScoreText;

    // Start is called before the first frame update
    void Start()
    {
        highSpeed.SetActive(false);
        lowSpeed.SetActive(false);
        multiplyScore.SetActive(false);
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

    public void LoadHomeScene()
    {
        SceneManager.LoadScene("HomeScene");
    }
}
