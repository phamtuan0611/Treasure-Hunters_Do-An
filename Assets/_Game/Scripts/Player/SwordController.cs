using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    public static SwordController instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }
        else
        {
            instance = this;
        }
    }

    [SerializeField] private Animator anim;
    private HashSet<GameObject> hitTargets = new HashSet<GameObject>();
    private Rigidbody2D rb;
    public bool isAttack;

    [SerializeField] private GameObject textPickUp;
    [SerializeField] private string textSword;
    private void OnEnable()
    {
        hitTargets.Clear();
        rb = GetComponent<Rigidbody2D>();
        isAttack = false;

        GameObject text = Instantiate(textPickUp, transform.position, Quaternion.identity);

        TextMeshPro tmp = text.GetComponent<TextMeshPro>();
        tmp.text = textSword;
        tmp.alpha = 1f;

        text.transform.localScale = Vector3.zero;
        text.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
        text.transform.DOMoveY(text.transform.position.y + 1.25f, 1f).SetEase(Ease.OutSine);

        tmp.DOFade(0f, 0.5f).SetDelay(0.7f);
        Destroy(text, 1.3f);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            rb.velocity = Vector2.zero;
            anim.SetBool("isGrounded", true);
        }

        if (other.CompareTag("Turtle"))
        {
            float speed = FindFirstObjectByType<PlayerController>().throwForce;
            Transform player = FindFirstObjectByType<PlayerController>().transform;
            rb.velocity = new Vector2(speed * (-1f) * player.lossyScale.x, 0);
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (!hitTargets.Contains(other.gameObject))
            {
                hitTargets.Add(other.gameObject);

                rb.velocity = Vector2.zero;
                anim.SetBool("isGrounded", true);
                isAttack = true;

                Destroy(gameObject);
            }
        }
    }
}
