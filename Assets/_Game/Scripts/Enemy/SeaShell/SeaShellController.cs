using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaShellController : MonoBehaviour
{
    public bool isDefeated;
    public float waitToDestroy;

    [SerializeField] private Animator anim;
    private float timeShoot;

    [SerializeField] private GameObject pearlBullet;
    // Start is called before the first frame update
    void Start()
    {
        timeShoot = 2.5f;
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

        timeShoot -= Time.deltaTime;
        if (timeShoot <= 0)
        {
            anim.SetTrigger("isAttack");

            StartCoroutine(DelayShoot());

            timeShoot = 2.5f;
        }
    }

    IEnumerator DelayShoot()
    {
        yield return new WaitForSeconds(0.5f);
        GameObject pearl = Instantiate(pearlBullet, transform.position, Quaternion.identity);
        pearl.transform.SetParent(transform);
        Destroy(pearl, 1f);
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

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FindFirstObjectByType<PlayerController>().Jump();
            anim.SetTrigger("isHitting");

            StartCoroutine(WaitAndDestroy());
        }

        if (AttackArea.instance.attack)
        {
            anim.SetTrigger("isHitting");

            StartCoroutine(WaitAndDestroy());
        }

        if (SwordController.instance != null)
        {
            if (SwordController.instance.isAttack)
            {
                anim.SetTrigger("isHitting");

                StartCoroutine(WaitAndDestroy());
            }
        }
    }

    private IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(0.1f);
        isDefeated = true;
    }
}
