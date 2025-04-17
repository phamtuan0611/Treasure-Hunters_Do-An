using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitPickup : MonoBehaviour
{
    [SerializeField] private GameObject effectFruit;
    [SerializeField] private int amount;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            bool isFruit = FindFirstObjectByType<PlayerController>().diamondPotion;

            if (isFruit)
            {
                CollectiblesManager.instance.GetCollectibleFruit(amount * 2);
            }
            else
            {
                CollectiblesManager.instance.GetCollectibleFruit(amount);
            }

            Destroy(gameObject);
            GameObject effect = Instantiate(effectFruit, transform.position, Quaternion.identity);
            Destroy(effect, 0.5f);
            //AudioManager.instance.allSFXPlayPitched(9);
        }
    }
}
