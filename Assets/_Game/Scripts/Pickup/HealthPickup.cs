using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] private int healthPickup;
    [SerializeField] private GameObject pickEffect, textPickUp;
    [SerializeField] private bool fullHealth;
    [SerializeField] private string textHeart;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (PlayerHealthController.instance.currentHealth != PlayerHealthController.instance.maxHealth)
            {
                if (fullHealth == true)
                {
                    PlayerHealthController.instance.AddHealth(PlayerHealthController.instance.maxHealth);
                }
                else
                {
                    PlayerHealthController.instance.AddHealth(healthPickup);
                }

                Destroy(gameObject);
                GameObject effect = Instantiate(pickEffect, transform.position, transform.rotation);

                GameObject text = Instantiate(textPickUp, transform.position, Quaternion.identity);

                TextMeshPro tmp = text.GetComponent<TextMeshPro>();
                tmp.text = textHeart;
                tmp.alpha = 1f;

                text.transform.localScale = Vector3.zero;
                text.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
                text.transform.DOMoveY(text.transform.position.y + 1.25f, 1f).SetEase(Ease.OutSine);

                tmp.DOFade(0f, 0.5f).SetDelay(0.7f);
                Destroy(text, 1.3f);

                Destroy(effect, 0.5f);
                //AudioManager.instance.allSFXPlay(10);
            }
        }
    }
}
