using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : EnemyMoving
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
        base.Update();
        if (isDefeated == true)
        {
            waitToDestroy -= Time.deltaTime;

            //foreach (Transform t in patrolPoints)
            //{
            //    t.SetParent(transform);
            //}

            if (waitToDestroy <= 0)
            {
                Destroy(gameObject);
                //Destroy(patrolPoints[0].gameObject);
                //Destroy(patrolPoints[1].gameObject);
                
                //AudioManager.instance.allSFXPlay(5);
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

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FindFirstObjectByType<PlayerController>().Jump();

            anim.SetTrigger("isHitting");

            isDefeated = true;
            
            //AudioManager.instance.allSFXPlay(6);
        }
    }
}
