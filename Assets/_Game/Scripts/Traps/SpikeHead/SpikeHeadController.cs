using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeHeadController : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerHealthController.instance.DamagePLayer();
        }
    }
}
