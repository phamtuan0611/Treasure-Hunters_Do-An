using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrunkAttack : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;

    private float timer;

    private bool isAttack;
    [SerializeField] private Animator anim;

    private float playerIn, playerOut;
    [SerializeField] private float timePeriod;

    void Start()
    {
        anim.GetComponent<Animator>();

        playerIn = timePeriod;
        playerOut = timePeriod;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttack == true)
        {
            playerIn -= Time.deltaTime;

            if (playerIn <= 0)
            {
                timer += Time.deltaTime;

                if (timer > 0.45f)
                {
                    StartCoroutine(DelayTimeShoot());

                    Shoot();
                }

                StartCoroutine(DelayTime());
            }

        }
        else
        {
            playerOut -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Attack");
            isAttack = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isAttack = false;
        }
    }

    IEnumerator DelayTimeShoot()
    {
        timer = 0;
        yield return new WaitForSeconds(0.4f);

    }

    IEnumerator DelayTime()
    {
        yield return new WaitForSeconds(0.25f);
        playerIn = timePeriod;
    }

    void Shoot()
    {
        GameObject spawnedBullet = Instantiate(bullet, new Vector3(bulletPos.position.x, bulletPos.position.y - 0.2f, bulletPos.position.z), Quaternion.identity);
        StartCoroutine(DestroyBullet(spawnedBullet));
    }

    IEnumerator DestroyBullet(GameObject bullet)
    {
        yield return new WaitForSeconds(2f);
        Destroy(bullet);
    }
}
