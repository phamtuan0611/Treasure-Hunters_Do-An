using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SmallMapPickup : MonoBehaviour
{
    [SerializeField] private GameObject effectSmallMap, textMap;
    [SerializeField] private string textSmallMap;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();
            if (inventory != null)
            {
                inventory.CollectSmallMap();
            }
            AudioManager.instance.PlaySFX(AudioManager.instance.pickUp);
            Destroy(gameObject);
            GameObject effect = Instantiate(effectSmallMap, transform.position, Quaternion.identity);

            GameObject text = Instantiate(textMap, transform.position, Quaternion.identity);

            TextMeshPro tmp = text.GetComponent<TextMeshPro>();
            tmp.text = textSmallMap;
            tmp.alpha = 1f;

            text.transform.localScale = Vector3.zero;
            text.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
            text.transform.DOMoveY(text.transform.position.y + 1.25f, 1f).SetEase(Ease.OutSine);

            tmp.DOFade(0f, 0.5f).SetDelay(0.7f);
            Destroy(text, 1.3f);

            Destroy(effect, 0.3f);
        }
    }
}
