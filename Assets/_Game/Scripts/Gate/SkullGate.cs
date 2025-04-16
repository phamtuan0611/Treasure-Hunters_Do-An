using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullGate : MonoBehaviour
{
    [SerializeField] private GameObject effectSkull;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();
            if (inventory != null)
            {
                inventory.CollectStone();
            }

            Destroy(gameObject);
            GameObject effect = Instantiate(effectSkull, transform.position, Quaternion.identity);
            Destroy(effect, 0.5f);
        }
    }
}
