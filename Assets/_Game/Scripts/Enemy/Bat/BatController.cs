using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : BlueBirdController
{
    [SerializeField] public Animator anim;
    private Coroutine attackCoroutine;
    // Start is called before the first frame update
    protected override void Start()
    {
        //anim = GetComponent<Animator>();
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        
    }
    protected override void OnReachedPatrolPoint()
    {
        anim.SetBool("isAttack", false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isAttack = true;
            anim.SetBool("isAttack", true);

            //attackCoroutine = StartCoroutine(DelayCelling());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (attackCoroutine != null)
            {
                StopCoroutine(attackCoroutine);
                attackCoroutine = null;
            }

            isAttack = false;

            if (Vector3.Distance(blueBrid.transform.position, patrolPoint.transform.position) <= 0.01f)
            {
                anim.SetBool("isAttack", false);
            }
        }
    }

    IEnumerator DelayCelling()
    {
        yield return new WaitForSeconds(0.6f);
        isAttack = true;
    }
}
