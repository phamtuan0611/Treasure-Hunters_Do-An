using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
    [Header("Các phần cổng đang đóng")]
    public GameObject[] closeParts;

    [Header("Các phần cổng mở ra")]
    public GameObject[] openParts;

    [Header("Di chuyển khi đủ đá")]
    public float moveDistance = 2f;
    public float moveSpeed = 2f;

    private bool gateFullyOpened = false;
    private Vector3 targetPosition;

    void Start()
    {
        // Tính vị trí cổng sẽ di chuyển xuống
        targetPosition = transform.position + Vector3.down * moveDistance;

        // Ban đầu: bật Close, tắt Open
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
