using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckController : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody2D rb;

    public Transform[] patrolPoints;
    private int currentIndex = 0;
    public float jumpForce = 5f;
    public float patrolDelay = 1f;

    public Transform player;
    public Transform zoneCenter;
    public float detectionRadius = 5f;
    private bool isPlayerInZone;

    public bool isDefeated;
    public float waitToDestroy = 0.5f;

    private bool isGrounded = true;

    private void Start()
    {
        foreach(Transform t in patrolPoints)
        {
            t.SetParent(null);
        }
        StartCoroutine(PatrolRoutine());
    }

    private void Update()
    {
        if (isDefeated)
        {
            waitToDestroy -= Time.deltaTime;
            if (waitToDestroy <= 0)
            {
                Destroy(gameObject);
                AudioManager.instance.PlaySFX(AudioManager.instance.enemyHit);
                // AudioManager.instance.allSFXPlay(5);
            }
            return;
        }

        CheckPlayerInZone();

        anim.SetFloat("ySpeed", rb.velocity.y);
        anim.SetBool("isGround", isGrounded);
    }

    private void CheckPlayerInZone()
    {
        if (player == null) return;

        float distance = Vector2.Distance(player.position, zoneCenter.position);
        isPlayerInZone = distance <= detectionRadius;
    }

    private IEnumerator PatrolRoutine()
    {
        bool movingForward = true;

        while (!isDefeated)
        {
            if (isPlayerInZone)
            {
                FlipDirection(player.position.x - transform.position.x);
                yield return JumpTo(player.position);
            }
            else
            {
                Vector3 nextPoint = patrolPoints[currentIndex].position;
                FlipDirection(nextPoint.x - transform.position.x);
                yield return JumpTo(nextPoint);

                if (movingForward)
                {
                    currentIndex++;
                    if (currentIndex >= patrolPoints.Length - 1)
                    {
                        currentIndex = patrolPoints.Length - 1;
                        movingForward = false;
                    }
                }
                else
                {
                    currentIndex--;
                    if (currentIndex <= 0)
                    {
                        currentIndex = 0;
                        movingForward = true;
                    }
                }
            }
            yield return new WaitForSeconds(patrolDelay);
        }
    }
    private void FlipDirection(float xDirection)
    {
        if (xDirection > 0)
            transform.localScale = new Vector3(-1f, 1f, 1f); 
        else if (xDirection < 0)
            transform.localScale = new Vector3(1f, 1f, 1f);
    }
    private IEnumerator JumpTo(Vector3 target)
    {
        if (!isGrounded) yield break;

        anim.SetTrigger("isJump");
        yield return new WaitForSeconds(0.3f); 

        Vector2 direction = (target - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * jumpForce, jumpForce);
        

        isGrounded = false;
        yield return new WaitUntil(() => isGrounded);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!isDefeated && other.gameObject.CompareTag("Player"))
        {
            PlayerHealthController.instance.DamagePLayer();
        }

        if (other.contacts[0].normal.y > 0.5f) 
        {
            isGrounded = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isDefeated) return;

        bool defeated = false;

        if (other.CompareTag("Player"))
        {
            FindFirstObjectByType<PlayerController>()?.Jump();
            defeated = true;
        }

        if (other.CompareTag("Sword") && other.gameObject.layer == LayerMask.NameToLayer("Player") && !AttackArea.instance.attack)
        {
            defeated = true;
        }

        if (SwordController.instance != null && SwordController.instance.isAttack)
        {
            defeated = true;
        }

        if (defeated)
        {
            anim.SetTrigger("isHitting");
            StartCoroutine(WaitAndDestroy());
            // AudioManager.instance.allSFXPlay(6);
        }
    }

    private IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(0.1f);
        isDefeated = true;
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        GetComponent<Collider2D>().enabled = false;
    }
}
