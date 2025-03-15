using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    private float timeInTrap = 0.1f;
    private bool isInTrap;

    private void Update()
    {
        if (isInTrap == true)
        {
            StartCoroutine(DamageOverTime());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealthController.instance.DamagePLayer();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInTrap = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInTrap = false;
        }
    }

    IEnumerator DamageOverTime()
    {
        if (isInTrap == true)
        {
            PlayerHealthController.instance.DamagePLayer();
            yield return new WaitForSeconds(timeInTrap);
        }
    }
}
