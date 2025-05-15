using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class FruitPickup : MonoBehaviour
{
    [SerializeField] private GameObject effectFruit, textPickUp;
    [SerializeField] private int amount;
    [SerializeField] private string textFruit;
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
            AudioManager.instance.PlaySFX(AudioManager.instance.pickUp);
            Destroy(gameObject);

            GameObject effect = Instantiate(effectFruit, transform.position, Quaternion.identity);
            
            GameObject text = Instantiate(textPickUp, transform.position, Quaternion.identity);
            
            TextMeshPro tmp = text.GetComponent<TextMeshPro>();
            tmp.text = textFruit;
            tmp.alpha = 1f;

            text.transform.localScale = Vector3.zero;
            text.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
            text.transform.DOMoveY(text.transform.position.y + 1.25f, 1f).SetEase(Ease.OutSine);

            tmp.DOFade(0f, 0.5f).SetDelay(0.7f);
            Destroy(text, 1.3f);

            Destroy(effect, 0.5f);
            //AudioManager.instance.allSFXPlayPitched(9);
        }
    }
}
