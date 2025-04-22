using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodBullet : MonoBehaviour
{
    private Rigidbody2D rb;
    public float force;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Transform scale = GetComponentInParent<TotemController>().transform;

        //transform.position = new Vector2(scale.position.x, scale.position.y - 0.75f);
        rb.velocity = new Vector2(scale.localScale.x * (-1f), 0).normalized * force;

        transform.SetParent(null);
    }
}
