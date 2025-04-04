using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryBigController : EnemyMoving
{
    private int changePhase;

    public bool isDefeated;

    public float waitToDestroy;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        changePhase = 0;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        ChangePhase();

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

    private void ChangePhase()
    {
        if (changePhase == 0)
        {
            anim.SetFloat("ChangePhase", 0);
            Debug.Log("Pig 1");
        }
        else if (changePhase == 1)
        {
            anim.SetFloat("ChangePhase", 1);

            moveSpeed = 6f;
            timeAtPoint = 0;

            Debug.Log("Pig 2");
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (changePhase == 0)
        {
            if (other.CompareTag("Player"))
            {

                Debug.Log("Change Phase: " + changePhase);
                FindFirstObjectByType<PlayerController>().Jump();

                //anim.SetBool("isRunning", true);

                //AudioManager.instance.allSFXPlay(6);
                changePhase = 1;
            }
        }
        else if (changePhase == 1)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Change Phase 1: " + changePhase);
                FindFirstObjectByType<PlayerController>().Jump();

                anim.SetTrigger("isHitting");

                isDefeated = true;
            }
        }
    }
}
