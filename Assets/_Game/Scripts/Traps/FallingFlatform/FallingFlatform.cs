using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FallingFlatform : MonoBehaviour
{
    [SerializeField] private Animator anim;
    private bool isFalling;
    private Vector3 originTransform;
    // Start is called before the first frame update
    void Start()
    {
        isFalling = false;
        originTransform = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFalling)
        {
            anim.SetBool("isFalling", true);
            transform.position = Vector3.MoveTowards(transform.position,
                new Vector3(transform.position.x, transform.position.y - 10f, transform.position.z), 5f * Time.deltaTime);
        }
        else
        {
            anim.SetBool("isFalling", false);
            transform.position = originTransform;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Falling());
        }
    }

    IEnumerator Falling()
    {
        yield return new WaitForSeconds(1.0f);
        isFalling = true;

        yield return new WaitForSeconds(3.0f);
        isFalling = false;
    }
}
