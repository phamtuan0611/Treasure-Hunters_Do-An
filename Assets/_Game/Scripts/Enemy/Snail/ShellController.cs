using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellController : EnemyMoving
{
    [SerializeField] protected bool isDefeated;
    [SerializeField] private float waitToDestroy;

    //[SerializeField] private Animator anim;
    private Rigidbody2D theRB;
    [SerializeField] private float hitSpeed;
    private int countHit = 0;

    void Awake()
    {
        theRB = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        theRB.velocity = new Vector2(1, 0);
    }

    // Update is called once per frame
    public override void Update()
    {
        if (countHit == 1)
        {
            base.Update();
            anim.SetBool("topHit", false);
        }

        if (isDefeated == true)
        {
            waitToDestroy -= Time.deltaTime;

            foreach (Transform t in patrolPoints)
            {
                t.SetParent(transform);
            }

            if (waitToDestroy <= 0)
            {
                Destroy(gameObject);
                //Destroy(patrolPoints[0].gameObject);
                //(patrolPoints[1].gameObject);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerHealthController.instance.DamagePLayer();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("Top hit");
            anim.SetBool("topHit", true);

            FindFirstObjectByType<PlayerController>().Jump();

            countHit++;

            if (countHit > 1)
            {
                isDefeated = true;
            }
        }
    }
}
