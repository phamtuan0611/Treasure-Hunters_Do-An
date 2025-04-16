using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    [SerializeField] private Animator anim;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetTrigger("isHitting");

            FindFirstObjectByType<PlayerController>().Jump();

            Destroy(gameObject, 0.2f);
        }
    }
}
