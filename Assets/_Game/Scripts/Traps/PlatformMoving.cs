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

    private Rigidbody2D rb;

    // Start is called before the first frame update
    public virtual void Start()
    {
        foreach (Transform t in patrolPoints)
        {
            t.SetParent(null);
        }

        waitCounter = timeAtPoint;
        rb = GetComponent<Rigidbody2D>();
    }

    public virtual void FixedUpdate()
    {
        Vector2 targetPosition = patrolPoints[currentPoint].position;
        Vector2 newPos = Vector2.MoveTowards(rb.position, targetPosition, moveSpeed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

        if (Vector2.Distance(rb.position, targetPosition) < .001f)
        {
            waitCounter -= Time.fixedDeltaTime;

            if (waitCounter <= 0)
            {
                currentPoint = (currentPoint + 1) % patrolPoints.Length;
                waitCounter = timeAtPoint;
            }
        }
    }
}
