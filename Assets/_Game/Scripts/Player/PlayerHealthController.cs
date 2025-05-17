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
    public float invincibilityCounter;

    private PlayerController thePlayer;
    private Rigidbody2D theRB;

    private PlayerInventory thePlayerInventory;
    public GameObject bubbleProtected;
    public float timeProtected;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        thePlayer = GetComponent<PlayerController>();
        theRB = GetComponent<Rigidbody2D>();
        thePlayerInventory = GetComponent<PlayerInventory>();

        bubbleProtected.SetActive(false);
        timeProtected = 10f;

        UIController.instance.UpdateHealthDisplay(currentHealth, maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (invincibilityCounter > 0)
        {
            invincibilityCounter -= Time.deltaTime;
        }

        if (thePlayerInventory.bubbleCount >= 1)
        {
            bubbleProtected.SetActive(true);
            timeProtected -= Time.deltaTime;

            invincibilityCounter = 1f;

            if (timeProtected <= 0)
            {
                thePlayerInventory.bubbleCount = 0;
                timeProtected = 10f;
                invincibilityCounter = 0f;
                bubbleProtected.SetActive(false);
            }
        }

    }

    public void DamagePLayer()
    {
        if (invincibilityCounter <= 0)
        {
            currentHealth--;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                thePlayer.isDead();

                StartCoroutine(Respawn());
            }
            else
            {
                invincibilityCounter = invincibilityLength;

                thePlayer.isKnock();
            }

            UIController.instance.UpdateHealthDisplay(currentHealth, maxHealth);
        }
    }

    public void AddHealth(int amountToAdd)
    {
        currentHealth += amountToAdd;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UIController.instance.UpdateHealthDisplay(currentHealth, maxHealth);
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(1f);
        LifeController.instance.Respawn();
    }
}
