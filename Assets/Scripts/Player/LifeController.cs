using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LifeController : MonoBehaviour
{
    public static LifeController instance;
    public int currentLive;
    private void Awake()
    {
        instance = this;
    }

    private PlayerController thePlayer;
    public float timeRespawn = 2f;
    public GameObject deathEffect, respawnEffect;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindFirstObjectByType<PlayerController>();

        //currentLive = InfoTracker.instance.currentLives;

        UpdateDisplay();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Respawn()
    {
        thePlayer.gameObject.SetActive(false);
        thePlayer.theRB.velocity = Vector2.zero; 

        currentLive--;

        if (currentLive > 0)
        {
            StartCoroutine(RespawnCo());
        }
        else
        {
            currentLive = 0;
            StartCoroutine(GameOver());
        }

        UpdateDisplay();

        Instantiate(deathEffect, thePlayer.transform.position, deathEffect.transform.rotation);
        //AudioManager.instance.allSFXPlay(11);
    }

    public IEnumerator RespawnCo()
    {
        yield return new WaitForSeconds(timeRespawn);

        thePlayer.transform.position = FindFirstObjectByType<CheckPointManager>().respawnPosition;
        PlayerHealthController.instance.AddHealth(PlayerHealthController.instance.maxHealth);

        thePlayer.gameObject.SetActive(true);
        Instantiate(respawnEffect, thePlayer.transform.position, Quaternion.identity); //Quaternion.identity la dat huong ve mac dinh
    }

    public IEnumerator GameOver()
    {
        yield return new WaitForSeconds(timeRespawn);

        //if (UIController.instance != null)
        //{
        //    UIController.instance.ShowGameOver();
        //}
    }

    public void AddLife()
    {
        currentLive++;
        UpdateDisplay();
        //AudioManager.instance.allSFXPlay(8);
    }

    public void UpdateDisplay()
    {
        //if (UIController.instance != null)
        //{
        //    UIController.instance.UpdateLiveDisplay(currentLive);
        //}
    }
}
