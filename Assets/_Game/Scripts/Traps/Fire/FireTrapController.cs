using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireTrapController : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject attackZone;
    private BoxCollider2D attackCollider;
    private void Start()
    {
        attackCollider = attackZone.GetComponent<BoxCollider2D>();
        attackCollider.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(On());
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(On());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Off());
        }
    }

    IEnumerator On()
    {
        anim.SetBool("isPlayer", true);

        yield return new WaitForSeconds(0.35f);

        attackCollider.enabled = true;
    }
    IEnumerator Off()
    {
        yield return new WaitForSeconds(1f);

        anim.SetBool("isPlayer", false);
        attackCollider.enabled = false;
    }
}
