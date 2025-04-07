using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : BlueBirdController
{
    [SerializeField] public Animator anim;

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetBool("isAttack", true);

            //StartCoroutine(DelayCelling());
            isAttack = true;
            Debug.Log("Bat: " + isAttack);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //anim.SetBool("isAttack", true);

            isAttack = false;
            Debug.Log("Bat Attack: " + isAttack);
        }
    }

    IEnumerator DelayCelling()
    {
        yield return new WaitForSeconds(0.6f);
        isAttack = true;
    }
}
