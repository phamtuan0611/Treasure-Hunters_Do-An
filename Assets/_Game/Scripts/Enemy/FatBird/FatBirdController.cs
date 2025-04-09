using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatBirdController : EnemyMoving
{
    public bool isDefeated;

    public float waitToDestroy;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        if (currentPoint == 0)
            transform.position = Vector3.MoveTowards(transform.position, patrolPoints[currentPoint].position, moveSpeed * Time.deltaTime);
        else if (currentPoint == 1)
            transform.position = Vector3.MoveTowards(transform.position, patrolPoints[currentPoint].position, 5 * moveSpeed * Time.deltaTime);
        //anim.SetBool("isFalling", false);

        if (Vector3.Distance(transform.position, patrolPoints[currentPoint].position) < .001f && currentPoint == 0)
        {
            anim.SetBool("isGround", false);
            waitCounter -= Time.deltaTime;

            if (waitCounter <= 0)
            {
                currentPoint++;
                if (currentPoint >= patrolPoints.Length)
                {
                    currentPoint = 0;
                }
                waitCounter = timeAtPoint;
                anim.SetBool("isFalling", true);
            }
        }
        
        if (Vector3.Distance(transform.position, patrolPoints[currentPoint].position) < .001f && currentPoint == 1)
        {
            anim.SetBool("isFalling", false);
            anim.SetBool("isGround", true);

            waitCounter -= Time.deltaTime;

            if (waitCounter <= 0)
            {
                currentPoint++;
                if (currentPoint >= patrolPoints.Length)
                {
                    currentPoint = 0;
                }
                waitCounter = timeAtPoint;
            }
        }



        if (isDefeated == true)
        {
            waitToDestroy -= Time.deltaTime;

            if (waitToDestroy <= 0)
            {
                Destroy(gameObject);

                //AudioManager.instance.allSFXPlay(5);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
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

            //AudioManager.instance.allSFXPlay(6);
        }

        if (SwordController.instance != null)
        {
            if (SwordController.instance.isAttack)
            {
                anim.SetTrigger("isHitting");

                StartCoroutine(WaitAndDestroy());

                //AudioManager.instance.allSFXPlay(6);
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
    private IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(0.1f);
        isDefeated = true;
    }
}
