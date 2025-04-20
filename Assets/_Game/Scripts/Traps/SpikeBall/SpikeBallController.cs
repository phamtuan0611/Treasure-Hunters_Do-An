using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBallController : MonoBehaviour
{
    public float angleRange = 45f;      
    public float speed = 1f;            

    private float startAngle;

    void Start()
    {
        startAngle = -angleRange;
    }

    void Update()
    {
        float angle = Mathf.PingPong(Time.time * speed * 2f, angleRange * 2f) - angleRange;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
