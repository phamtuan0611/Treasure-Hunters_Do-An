using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampolineController : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private float speedBounce;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetTrigger("isPlayer");
            AudioManager.instance.PlaySFX(AudioManager.instance.trampoline);
            Rigidbody2D theRB = other.GetComponent<Rigidbody2D>();
            theRB.velocity = new Vector2(theRB.velocity.x, speedBounce);
        }
    }
}
