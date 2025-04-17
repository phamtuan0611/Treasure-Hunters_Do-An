using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int stoneCount = 0;

    public int keyCount = 0;

    public int bubbleCount = 0;

    public void CollectStone()
    {
        stoneCount++;
        UIController.instance.UpdateSkullGate(stoneCount);
    }

    public void CollectKey()
    {
        keyCount++;
    }

    public void CollectBubble()
    {
        bubbleCount++;
        PlayerHealthController.instance.timeProtected = 10f;
        Debug.Log("Bubble Count Inventory: " + bubbleCount);
    }
}
