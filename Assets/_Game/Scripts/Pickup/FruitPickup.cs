using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitPickup : MonoBehaviour
{
    [SerializeField] private GameObject effectFruit;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            GameObject effect = Instantiate(effectFruit, transform.position, Quaternion.identity);
            Destroy(effect, 0.3f);
            //AudioManager.instance.allSFXPlayPitched(9);
        }
    }
}
