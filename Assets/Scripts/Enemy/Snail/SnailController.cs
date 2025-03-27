using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailController : MonoBehaviour
{
    [SerializeField] private GameObject shellShall, snailNShell;
    [SerializeField] private float speed;

    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            shellShall.SetActive(true);
            

            shellShall.transform.SetParent(null);

            //snailNShell.SetActive(true);
            //snailNShell.transform.SetParent(null);
        }
    }
}
