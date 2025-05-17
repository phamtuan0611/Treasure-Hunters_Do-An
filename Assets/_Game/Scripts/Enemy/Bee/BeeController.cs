using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeController : MonoBehaviour
{
    [SerializeField] private Animator anim;

    public bool isDefeated;
    public float waitToDestroy;

    private float timeAttack;

    public GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        timeAttack = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDefeated == true)
        {
            waitToDestroy -= Time.deltaTime;

            if (waitToDestroy <= 0)
            {
                Destroy(gameObject);
                AudioManager.instance.PlaySFX(AudioManager.instance.enemyHit);
            }
        }

        timeAttack -= Time.deltaTime;
        if (timeAttack <= 0)
        {
            anim.SetTrigger("isAttack");

            StartCoroutine(SpawnBullet());

            timeAttack = 3f;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FindFirstObjectByType<PlayerController>().Jump();
            anim.SetTrigger("isHitting");
            isDefeated = true;
        }

        if (AttackArea.instance != null)
        {
            if (AttackArea.instance.attack)
            {
                anim.SetTrigger("isHitting");
                isDefeated = true;
            }
        }

        if (SwordController.instance != null)
        {
            if (SwordController.instance.isAttack)
            {
                anim.SetTrigger("isHitting");
                isDefeated = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (isDefeated == false)
            {
                PlayerHealthController.instance.DamagePLayer();
            }
        }
    }

    IEnumerator SpawnBullet()
    {
        yield return new WaitForSeconds(0.8f);

        GameObject bulletBee = Instantiate(bullet, transform.position, Quaternion.identity);

        Rigidbody2D rb = bulletBee.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            float direction = transform.localScale.y;
            rb.velocity = new Vector2(0f, -direction * 5f);
        }
        Destroy(bulletBee, 3f);
    }
}
