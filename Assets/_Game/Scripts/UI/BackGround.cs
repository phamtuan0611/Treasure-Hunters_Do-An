using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackGround : MonoBehaviour
{
    public float scrollSpeed = 0.2f;
    private RawImage image;

    void Start()
    {
        image = GetComponent<RawImage>();
    }

    void Update()
    {
        Rect uv = image.uvRect;
        uv.y += scrollSpeed * Time.deltaTime;
        image.uvRect = uv;
    }
}
