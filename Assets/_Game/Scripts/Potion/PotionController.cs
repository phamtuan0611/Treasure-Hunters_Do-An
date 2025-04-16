using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionController : MonoBehaviour
{
    [SerializeField] private GameObject effectPotion;

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            GameObject effect = Instantiate(effectPotion, transform.position, Quaternion.identity);

            Destroy(effect, 0.5f);
        }
    }
}
