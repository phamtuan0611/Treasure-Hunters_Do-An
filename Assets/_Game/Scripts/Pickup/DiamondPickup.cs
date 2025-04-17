using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondPickup : MonoBehaviour
{
    [SerializeField] private GameObject effectDiamond;
    [SerializeField] private int amount;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            bool isDiamond = FindFirstObjectByType<PlayerController>().diamondPotion;

            if (isDiamond)
            {
                CollectiblesManager.instance.GetCollectibleDiamond(amount * 2);
            }
            else
            {
                CollectiblesManager.instance.GetCollectibleDiamond(amount);
            }

            Destroy(gameObject);
            GameObject effect = Instantiate(effectDiamond, transform.position, Quaternion.identity);
            Destroy(effect, 0.5f);
            //AudioManager.instance.allSFXPlayPitched(9);
        }
    }
}
