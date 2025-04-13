using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    public static AttackArea instance;

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

    private HashSet<GameObject> hitTargets = new HashSet<GameObject>();
    public bool attack;

    private void OnEnable()
    {
        hitTargets.Clear();
        attack = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            //Destroy(other.gameObject);
            Debug.Log("Why");
            if (!hitTargets.Contains(other.gameObject))
            {
                Debug.Log("123456");
                hitTargets.Add(other.gameObject);
                attack = true;
                Debug.Log("ATTACKATTACK: " + attack);
            }
        }
        else
        {
            //attack = false;
            StartCoroutine(DelayAttackReset());
        }
    }
    private IEnumerator DelayAttackReset()
    {
        yield return new WaitForSeconds(0.1f);

        attack = false;
    }
}
