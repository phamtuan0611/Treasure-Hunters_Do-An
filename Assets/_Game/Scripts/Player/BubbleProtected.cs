using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BubbleProtected : MonoBehaviour
{
    [SerializeField] private GameObject textPickUp;
    [SerializeField] private string textBubble;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();
            if (inventory != null)
            {
                inventory.CollectBubble();
            }

            Destroy(gameObject);

            GameObject text = Instantiate(textPickUp, transform.position, Quaternion.identity);

            TextMeshPro tmp = text.GetComponent<TextMeshPro>();
            tmp.text = textBubble;
            tmp.alpha = 1f;

            text.transform.localScale = Vector3.zero;
            text.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
            text.transform.DOMoveY(text.transform.position.y + 1.25f, 1f).SetEase(Ease.OutSine);

            tmp.DOFade(0f, 0.5f).SetDelay(0.7f);
            Destroy(text, 1.3f);
        }
    }
}
