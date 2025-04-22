using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabbyAttack : MonoBehaviour
{
    private float originSpeed, timeAttack;
    private bool isPlayer;
    private Vector3 fixedWorldPos;

    [SerializeField] private Animator anim;

    [SerializeField] private GameObject attack;
    private BoxCollider2D attackCollider;
    private void Start()
    {
        originSpeed = GetComponentInParent<EnemyController>().moveSpeed;
        fixedWorldPos = transform.position;

        timeAttack = 2f;

        attackCollider = attack.GetComponent<BoxCollider2D>();
        attackCollider.enabled = false;
    }

    private void Update()
    {
        transform.position = fixedWorldPos;
        if (isPlayer)
        {
            timeAttack -= Time.deltaTime;

            GetComponentInParent<EnemyController>().moveSpeed = originSpeed * 2;

            if (timeAttack <= 0)
            {
                anim.SetTrigger("isPlayer");
                anim.SetBool("isMoving", false);
                
                StartCoroutine(DisableAttack());

                timeAttack = 2f;
            }
            else
            {
                //attackCollider.enabled = false;
                anim.SetBool("isMoving", true);
            }
        }
        else
        {
            GetComponentInParent<EnemyController>().moveSpeed = originSpeed;
            //anim.SetBool("isMoving", true);
        }
    }

    IEnumerator DisableAttack()
    {
        attackCollider.enabled = true;
        yield return new WaitForSeconds(0.3f);
        attackCollider.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayer = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayer = false;
        }
    }
}
