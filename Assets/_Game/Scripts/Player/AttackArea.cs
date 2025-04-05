using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private HashSet<GameObject> hitTargets = new HashSet<GameObject>();

    private void OnEnable()
    {
        hitTargets.Clear(); 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            //Destroy(other.gameObject);
            if (!hitTargets.Contains(other.gameObject))
            {
                hitTargets.Add(other.gameObject);

                Debug.Log("Hit enemy: " + other.name);
            }
        }
    }
}
