using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PotionController : MonoBehaviour
{
    [SerializeField] private GameObject effectPotion, textPickUp;
    [SerializeField] private string textPotion;

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            GameObject effect = Instantiate(effectPotion, transform.position, Quaternion.identity);

            GameObject text = Instantiate(textPickUp, transform.position, Quaternion.identity);

            TextMeshPro tmp = text.GetComponent<TextMeshPro>();
            tmp.text = textPotion;
            tmp.alpha = 1f;

            text.transform.localScale = Vector3.zero;
            text.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
            text.transform.DOMoveY(text.transform.position.y + 1.25f, 1f).SetEase(Ease.OutSine);

            tmp.DOFade(0f, 0.5f).SetDelay(0.7f);
            Destroy(text, 1.3f);

            Destroy(effect, 0.5f);
        }
    }
}
