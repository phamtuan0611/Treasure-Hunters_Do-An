using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestKeyController : MonoBehaviour
{
    [SerializeField] private Animator anim;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();
            if (inventory != null && inventory.keyCount >= 1)
            {
                anim.SetTrigger("isHaveKey");
            }
        }
    }
}
