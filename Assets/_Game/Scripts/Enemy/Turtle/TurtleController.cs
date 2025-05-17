using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleController : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private float timeChangePhase;

    private BoxCollider2D boxCollider;
    private CapsuleCollider2D capsuleCollider;

    public bool isDefeated;
    public float waitToDestroy;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();

        timeChangePhase = 6f;
    }

    // Update is called once per frame
    void Update()
    {
        timeChangePhase -= Time.deltaTime;

        if (timeChangePhase > 0)
        {
            anim.SetBool("isSpike", true);
            
            boxCollider.enabled = false;
            capsuleCollider.enabled = true;

            gameObject.tag = "Turtle";
            gameObject.layer = LayerMask.NameToLayer("Default");
        }
        else if (timeChangePhase <= -6f)
        {
            timeChangePhase = 6f;
        }
        else if (timeChangePhase <= 0)
        {
            anim.SetBool("isSpike", false);

            boxCollider.enabled = true;
            capsuleCollider.enabled = false;

            gameObject.tag = "Untagged";
            gameObject.layer = LayerMask.NameToLayer("Enemy");
        }

        //Dead
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
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (isDefeated == false)
            {
                PlayerHealthController.instance.DamagePLayer();
            }
        }

        if (other.gameObject.CompareTag("Sword") && !AttackArea.instance.attack)
        {
            
            anim.SetTrigger("isHitting");

            StartCoroutine(WaitAndDestroy());
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FindFirstObjectByType<PlayerController>().Jump();
            anim.SetTrigger("isHitting");

            StartCoroutine(WaitAndDestroy());
        }

        if (gameObject.tag != "Turtle")
        {
            if (other.CompareTag("Sword") && other.gameObject.layer == LayerMask.NameToLayer("Player") && !AttackArea.instance.attack)
            {

                anim.SetTrigger("isHitting");

                StartCoroutine(WaitAndDestroy());
            }
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
