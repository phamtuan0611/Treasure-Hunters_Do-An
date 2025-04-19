using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBallController : MonoBehaviour
{
    [SerializeField] private float initialForce = 5f;

    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(initialForce, 0), ForceMode2D.Impulse);
    }
}
