using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestKeyController : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject key, pickUp;
    private BoxCollider2D boxCollider;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();
            Destroy(key.gameObject);

            if (inventory != null && inventory.keyCount >= 1)
            {
                anim.SetTrigger("isHaveKey");
                boxCollider.enabled = false;


                StartCoroutine(MoveUp());
            }
        }
    }

    private IEnumerator MoveUp()
    {
        yield return new WaitForSeconds(0.9f);

        GameObject treasure = Instantiate(pickUp, transform.position, Quaternion.identity);

        float duration = 0.5f;
        float height = 1f;
        Vector3 startPos = treasure.transform.position;
        Vector3 endPos = startPos + new Vector3(0, height, 0);
        float elapsed = 0f;

        while (elapsed < duration)
        {
            treasure.transform.position = Vector3.Lerp(startPos, endPos, elapsed / duration);
            elapsed += Time.deltaTime; 
            yield return null;
        }

        treasure.transform.position = endPos;
    }
}
