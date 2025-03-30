using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailNShell : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //theRB = GetComponent<Rigidbody2D>();
        //theRB.velocity = new Vector2(-4, 3);
    }

    // Update is called once per frame
    void Update()
    {
        //base.Update();
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
            Color theColor = GetComponent<SpriteRenderer>().color;
            theColor = Color.white;

            //isDefeated = true;
        }
    }
}
