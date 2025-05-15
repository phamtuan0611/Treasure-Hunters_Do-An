using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject item;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetTrigger("isHitting");
            FindFirstObjectByType<PlayerController>().Jump();
            AudioManager.instance.PlaySFX(AudioManager.instance.breakBox);
            StartCoroutine(DelaySpawn());
            Destroy(gameObject, 0.2f);
        }
    }

    IEnumerator DelaySpawn()
    {
        yield return new WaitForSeconds(0.1f);
        Instantiate(item, new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z), Quaternion.identity);
    }
}
