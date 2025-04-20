using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChameleonAttack : MonoBehaviour
{
    [SerializeField] private Animator anim;
    private bool isAttacking = false;
    private bool isPlayerInside = false; 
    private Collider2D playerCollider;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;
            if (!isAttacking)
            {
                playerCollider = other;
                StartCoroutine(AttackLoop());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
        }
    }

    IEnumerator AttackLoop()
    {
        isAttacking = true;

        while (isPlayerInside)
        {
            anim.SetTrigger("isAttack");

            yield return new WaitForSeconds(0.5f);

            DamagePlayer dp = gameObject.AddComponent<DamagePlayer>();
            dp.SendMessage("OnTriggerEnter2D", playerCollider, SendMessageOptions.DontRequireReceiver);

            dp.SendMessage("OnTriggerStay2D", playerCollider, SendMessageOptions.DontRequireReceiver);
            
            yield return new WaitForSeconds(0.3f);

            Destroy(dp);

            yield return null;
            yield return new WaitForSeconds(1f);
        }

        isAttacking = false;
    }
}
