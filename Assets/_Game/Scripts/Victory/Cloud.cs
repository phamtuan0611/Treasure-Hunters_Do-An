using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public float speed = 20f; 
    public float resetPositionX = -2778f; 
    public float startPositionX = 2778f;  

    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        if (transform.localPosition.x < resetPositionX)
        {
            Vector3 newPos = transform.localPosition;
            newPos.x = startPositionX;
            transform.localPosition = newPos;
        }
    }
}
