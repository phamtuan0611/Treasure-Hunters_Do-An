using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
    [Header("Gate Close")]
    public GameObject[] closeParts;

    [Header("Gate Open")]
    public GameObject[] openParts;

    [Header("Move Gate")]
    public float moveDistance = 3f;
    public float moveSpeed = 2f;

    private bool gateFullyOpened = false;
    private Vector3 targetPosition;

    void Start()
    {
        targetPosition = transform.position + Vector3.down * moveDistance;

        for (int i = 0; i < 3; i++)
        {
            if (closeParts[i]) closeParts[i].SetActive(true);
            if (openParts[i]) openParts[i].SetActive(false);
        }
    }

    public void OpenGate(int stones)
    {
        for (int i = 0; i < stones && i < 3; i++)
        {
            if (closeParts[i]) closeParts[i].SetActive(false);
            if (openParts[i]) openParts[i].SetActive(true);
        }

        if (stones >= 3 && !gateFullyOpened)
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.openGate);
            gateFullyOpened = true;
        }
    }

    void Update()
    {
        if (gateFullyOpened)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }
}
