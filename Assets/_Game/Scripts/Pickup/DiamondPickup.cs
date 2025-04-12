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
            CollectiblesManager.instance.GetCollectibleDiamond(amount);
            Destroy(gameObject);
            GameObject effect = Instantiate(effectDiamond, transform.position, Quaternion.identity);
            Destroy(effect, 0.5f);
            //AudioManager.instance.allSFXPlayPitched(9);
        }
    }
}
