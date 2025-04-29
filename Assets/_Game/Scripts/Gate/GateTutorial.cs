using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GateTutorial : MonoBehaviour
{
    public float moveDistance = 3f;
    public float moveSpeed = 2f;

    private bool gateFullyOpened;
    private Vector3 targetPosition, originalTransform;
    // Start is called before the first frame update
    void Start()
    {
        originalTransform = transform.position;
        targetPosition = transform.position + Vector3.down * moveDistance;
        gateFullyOpened = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gateFullyOpened)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) <= 0.001f)
                gateFullyOpened = true;
        }
        else
        {
            StartCoroutine(DelayMove());
        }
    }

    IEnumerator DelayMove()
    {
        transform.position = originalTransform;
        
        yield return new WaitForSeconds(0.5f);
        
        gateFullyOpened = false;
    }
}
