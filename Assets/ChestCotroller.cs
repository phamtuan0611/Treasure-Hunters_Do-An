using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestCotroller : MonoBehaviour
{
    [SerializeField] private Animator anim;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetTrigger("isPlayer");
        }
    }
}
