using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class TitleMapController : MonoBehaviour
{
    [SerializeField] private Tilemap tileMap;
    [SerializeField] private float fadeSpeed;
    [SerializeField] private bool fadingToBlack, fadingFromBlack;

    private void Update()
    {
        if (fadingFromBlack)
        {
            tileMap.color = new Color(tileMap.color.r, tileMap.color.g, tileMap.color.b, Mathf.MoveTowards(tileMap.color.a, 0f, fadeSpeed * Time.deltaTime));
        }

        if (fadingToBlack)
        {
            tileMap.color = new Color(tileMap.color.r, tileMap.color.g, tileMap.color.b, Mathf.MoveTowards(tileMap.color.a, 1f, fadeSpeed * Time.deltaTime));
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player in");
            FadeFromBlack();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player out");
            FadeToBlack();
        }
    }

    public void FadeFromBlack()
    {
        fadingToBlack = false;
        fadingFromBlack = true;
    }

    public void FadeToBlack()
    {
        fadingToBlack = true;
        fadingFromBlack = false;
    }
}
