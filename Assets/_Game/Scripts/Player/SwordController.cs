using System.Collections;
using System.Collections.Generic;
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
    private void OnEnable()
    {
        hitTargets.Clear();
        rb = GetComponent<Rigidbody2D>();
        isAttack = false;
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
            rb.velocity = new Vector2(speed * (-1f), 0);
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            //Destroy(other.gameObject);
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
