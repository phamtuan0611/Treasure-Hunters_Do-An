using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailNShell : MonoBehaviour
{
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
        }
    }
}
