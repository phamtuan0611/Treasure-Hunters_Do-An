using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    [SerializeField] private GameObject effectKey;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();
            if (inventory != null)
            {
                inventory.CollectKey();
            }

            Destroy(gameObject);
            GameObject effect = Instantiate(effectKey, transform.position, Quaternion.identity);
            Destroy(effect, 0.5f);
        }
    }
}
