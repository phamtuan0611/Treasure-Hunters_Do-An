using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    private void Awake()
    {
        instance = this;
    }

    public int currentHealth, maxHealth;

    [SerializeField] private Animator anim;
    [SerializeField] private float invincibilityLength = 1f;
    private float invincibilityCounter;

    private PlayerController thePlayer;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        thePlayer = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (invincibilityCounter > 0)
        {
            invincibilityCounter -= Time.deltaTime;

            //if (invincibilityCounter <= 0)
            //{
            //    anim.SetTrigger("hit");
            //}
        }
    }

    public void DamagePLayer()
    {
        if (invincibilityCounter <= 0)
        {
            //invincibilityCounter = invincibilityLength;

            currentHealth--;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                //gameObject.SetActive(false);
                //LifeController.instance.Respawn();
            }
            else
            {
                invincibilityCounter = invincibilityLength;

                //theSR.color = fadeColor;

                thePlayer.isKnock();
                //AudioManager.instance.allSFXPlay(13);
            }

            //UIController.instance.UpdateHealthDisplay(currentHealth, maxHealth);
        }

    }

    public void AddHealth(int amountToAdd)
    {
        currentHealth += amountToAdd;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        //UIController.instance.UpdateHealthDisplay(currentHealth, maxHealth);
    }
}
