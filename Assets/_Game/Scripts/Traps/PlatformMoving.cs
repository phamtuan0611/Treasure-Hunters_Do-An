using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMoving : MonoBehaviour
{
    [SerializeField] private Transform[] patrolPoints;
    private int currentPoint;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float timeAtPoint;
    protected float waitCounter;

    // Start is called before the first frame update
    public virtual void Start()
    {
        foreach (Transform t in patrolPoints)
        {
            t.SetParent(null);
        }

        waitCounter = timeAtPoint;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, patrolPoints[currentPoint].position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, patrolPoints[currentPoint].position) < .001f)
        {
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
    }
}
