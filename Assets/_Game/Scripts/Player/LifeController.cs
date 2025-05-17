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

        UpdateDisplay();
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
    }

    public IEnumerator RespawnCo()
    {
        yield return new WaitForSeconds(timeRespawn);

        thePlayer.transform.position = FindFirstObjectByType<CheckPointManager>().respawnPosition;
        PlayerHealthController.instance.AddHealth(PlayerHealthController.instance.maxHealth);

        thePlayer.gameObject.SetActive(true);
        Instantiate(respawnEffect, thePlayer.transform.position, Quaternion.identity);
    }

    public IEnumerator GameOver()
    {
        yield return new WaitForSeconds(timeRespawn);
    }

    public void AddLife()
    {
        currentLive++;
        UpdateDisplay();
    }

    public void UpdateDisplay()
    {
        if (UIController.instance != null)
        {
            UIController.instance.UpdateLiveDisplay(currentLive);
        }
    }
}
