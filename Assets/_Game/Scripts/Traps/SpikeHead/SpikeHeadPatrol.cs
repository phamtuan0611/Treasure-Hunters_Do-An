using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeHeadPatrol : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private bool isAttack, isAudio;
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private int currentPoint;
    [SerializeField] private float speed;
    [SerializeField] private float waitTime, timeAtPoint;
    [SerializeField] private GameObject particleObject;

    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask whatIsGround;
    private bool isGrounded;


    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform ptPoint in patrolPoints)
        {
            ptPoint.SetParent(null);
        }

        waitTime = timeAtPoint;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttack == true)
        {
            currentPoint = 1;
            isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, whatIsGround);
            anim.SetBool("isAttack", isAttack);
            anim.SetBool("isGround", isGrounded);
            transform.position = Vector3.MoveTowards(transform.position, patrolPoints[currentPoint].position, speed * Time.deltaTime);
            
            if (Vector3.Distance(transform.position, patrolPoints[currentPoint].position) < 0.001f && isAudio == false)
            {
                Instantiate(particleObject, transform.position, Quaternion.identity);
                isAudio = true;
            }

            waitTime -= Time.deltaTime;

            if (waitTime <= 0)
            {
                isAudio = false;
                isAttack = false;
                waitTime = timeAtPoint;
            }
        }

        if (isAttack == false)
        {
            currentPoint = 0;

            anim.SetBool("isAttack", isAttack);
            transform.position = Vector3.MoveTowards(transform.position, patrolPoints[currentPoint].position, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isAttack = true;
        }
    }
}
