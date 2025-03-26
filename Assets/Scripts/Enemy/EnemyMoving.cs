using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoving : MonoBehaviour
{
    [SerializeField] protected Transform[] patrolPoints;
    private int currentPoint;
    [SerializeField] protected float moveSpeed;
    [SerializeField] private float timeAtPoint;
    private float waitCounter;
    public EnemyController theEnemy;
    [SerializeField] protected Animator anim;

    // Start is called before the first frame update
    public virtual void Start()
    {
        foreach (Transform t in patrolPoints)
        {
            t.SetParent(null);
        }

        waitCounter = timeAtPoint;

        anim = GetComponent<Animator>();
        anim.SetBool("isMoving", true);
    }

    // Update is called once per frame
    public virtual void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, patrolPoints[currentPoint].position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, patrolPoints[currentPoint].position) < .001f)
        {
            anim.SetBool("isMoving", false);
            waitCounter -= Time.deltaTime;

            if (waitCounter <= 0)
            {
                currentPoint++;
                if (currentPoint >= patrolPoints.Length)
                {
                    currentPoint = 0;
                }
                waitCounter = timeAtPoint;
                anim.SetBool("isMoving", true);

                //Dao huong
                transform.localScale = new Vector3(transform.localScale.x * (-1f), transform.localScale.y, transform.localScale.z);
            }
        }
    }
}
