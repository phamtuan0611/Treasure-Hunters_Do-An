using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FierceToothAttack : MonoBehaviour
{
    private float originSpeed, timeAttack;
    private bool isPlayer;
    private Vector3 fixedWorldPos;

    [SerializeField] private Animator anim;
    private void Start()
    {
        originSpeed = GetComponentInParent<EnemyController>().moveSpeed;
        fixedWorldPos = transform.position;

        timeAttack = 2f;
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

                timeAttack = 2f;
            }
            else
            {
                anim.SetBool("isMoving", true);
            }
        }
        else
        {
            GetComponentInParent<EnemyController>().moveSpeed = originSpeed;
            anim.SetBool("isMoving", true);
        }
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
