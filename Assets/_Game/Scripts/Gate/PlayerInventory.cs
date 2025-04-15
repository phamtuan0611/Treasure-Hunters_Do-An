using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int stoneCount = 0;

    public void CollectStone()
    {
        stoneCount++;
    }
}
