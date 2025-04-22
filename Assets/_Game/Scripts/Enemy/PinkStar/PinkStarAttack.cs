using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkStarAttack : MonoBehaviour
{
    private float originSpeed;
    private bool isPlayer;
    private Vector3 fixedWorldPos;

    [SerializeField] private Animator anim;
    private void Start()
    {
        originSpeed = GetComponentInParent<EnemyController>().moveSpeed;
        fixedWorldPos = transform.position;
    }

    private void Update()
    {
        transform.position = fixedWorldPos;
        if (isPlayer)
        {
            GetComponentInParent<EnemyController>().moveSpeed = originSpeed * 2;
            anim.SetBool("isPlayer", true);
            anim.SetBool("isMoving", false);
        }
        else
        {
            GetComponentInParent<EnemyController>().moveSpeed = originSpeed;
            anim.SetBool("isPlayer", false);
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
