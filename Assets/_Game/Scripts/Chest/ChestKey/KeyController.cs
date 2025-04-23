using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class KeyController : MonoBehaviour
{
    [SerializeField] private GameObject effectKey;
    private Transform targetFollow;
    private bool isFollowing = false;
    private Vector3 offset = new Vector3(-1.2f, 0.6f, 0f);
    private float speed = 3f;

    private void Update()
    {
        if (isFollowing && targetFollow != null)
        {
            transform.SetParent(targetFollow.transform);
            Vector3 targetPos = targetFollow.position + offset;
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPos) <= 0.001)
            {
                isFollowing = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();
            if (inventory != null)
            {
                inventory.CollectKey();
            }

            //Destroy(gameObject);
            targetFollow = other.transform;
            isFollowing = true;
            float direction = Mathf.Sign(other.transform.localScale.x);
            offset.x = Mathf.Abs(offset.x) * -direction;
            //gameObject.transform.SetParent(other.transform);

            GameObject effect = Instantiate(effectKey, transform.position, Quaternion.identity);
            Destroy(effect, 0.5f);
        }
    }

    private void OnDestroy()
    {
        GameObject effect = Instantiate(effectKey, transform.position, Quaternion.identity);
        Destroy(effect, 0.5f);
    }
}
