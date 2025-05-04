using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotMap : MonoBehaviour
{
    public float blinkInterval = 0.2f; 

    private SpriteRenderer spriteRenderer;
    private bool isBlinking = true;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(Blink());
    }

    IEnumerator Blink()
    {
        while (isBlinking)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(blinkInterval);
        }
    }

    public void StopBlinking()
    {
        isBlinking = false;
        spriteRenderer.enabled = true; 
    }
}
