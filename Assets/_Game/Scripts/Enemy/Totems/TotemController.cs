using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemController : MonoBehaviour
{
    [SerializeField] private Animator anim;

    private float timeAttack;
    public float timeDelay;
    [SerializeField] private GameObject woodBullet;

    public bool isDefeated;
    public float waitToDestroy;

    // Start is called before the first frame update
    void Start()
    {
        timeAttack = timeDelay + 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        timeAttack -= Time.deltaTime;

        if (timeAttack <= 0)
        {
            anim.SetTrigger("isAttack");
            StartCoroutine(DelayShoot());

            timeAttack = timeDelay;
        }

        if (isDefeated == true)
        {
            waitToDestroy -= Time.deltaTime;

            if (waitToDestroy <= 0)
            {
                Destroy(gameObject);
                AudioManager.instance.PlaySFX(AudioManager.instance.enemyHit);
            }
        }
    }

    IEnumerator DelayShoot()
    {
        yield return new WaitForSeconds(0.4f);
        GameObject bullet = Instantiate(woodBullet, new Vector3(transform.position.x, transform.position.y - 0.55f, transform.position.z), Quaternion.identity);
        bullet.transform.SetParent(this.transform);
        
        Destroy(bullet, 1.5f);
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

        TotemPiece piece = GetComponent<TotemPiece>();
        if (piece != null)
        {
            piece.OnDestroyed();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
