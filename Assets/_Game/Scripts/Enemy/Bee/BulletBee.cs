using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBee : MonoBehaviour
{
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            rb.velocity = Vector2.zero;
        }

        if (other.CompareTag("Player"))
        {
            PlayerHealthController.instance.DamagePLayer();
        }
    }
}
