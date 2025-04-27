using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallMapPickup : MonoBehaviour
{
    [SerializeField] private GameObject effectSmallMap;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();
            if (inventory != null)
            {
                inventory.CollectSmallMap();
            }

            Destroy(gameObject);
            GameObject effect = Instantiate(effectSmallMap, transform.position, Quaternion.identity);
            Destroy(effect, 0.5f);
        }
    }
}
