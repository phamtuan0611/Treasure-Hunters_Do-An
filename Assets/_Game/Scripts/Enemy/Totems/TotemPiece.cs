using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemPiece : MonoBehaviour
{
    public TotemPiece pieceTop;
    public TotemPiece pieceAbove; 
    public TotemPiece pieceBelow; 

    private Vector3 piecePosition;

    public void OnDestroyed()
    {
        if (pieceAbove != null)
        {
            piecePosition = pieceAbove.transform.position;

            pieceAbove.FallTo(transform.position);
            StartCoroutine(DelayTotemTop());

            if (pieceBelow != null)
            {
                pieceBelow.pieceAbove = pieceAbove;
                pieceAbove.pieceBelow = pieceBelow;
            }
            else
            {
                pieceAbove.pieceBelow = null;
            }

            pieceAbove = null;
        }
        
        //Destroy(gameObject);
    }

    public void FallTo(Vector3 targetPosition)
    {
        var rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            StartCoroutine(MoveSmoothly(targetPosition));
        }
    }

    private IEnumerator MoveSmoothly(Vector3 targetPosition)
    {
        float time = 0;
        Vector3 start = transform.position;
        while (time < 1f)
        {
            time += Time.deltaTime * 2f;
            transform.position = Vector3.Lerp(start, targetPosition, time);
            yield return null;
        }
    }

    IEnumerator DelayTotemTop()
    {
        yield return new WaitForSeconds(0.05f);
        if (pieceTop != null)
        {
            pieceTop.FallTo(piecePosition);
        }
    }
}
