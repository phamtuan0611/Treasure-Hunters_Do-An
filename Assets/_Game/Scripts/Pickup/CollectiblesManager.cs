using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblesManager : MonoBehaviour
{
    public static CollectiblesManager instance;

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] public int fruitCount, diamondCount;
    [SerializeField] public int extraLifeThreshold;

    public void GetCollectibleFruit(int amount)
    {
        fruitCount += amount;
        if (UIController.instance != null)
        {
            UIController.instance.UpdateFruitDisplay(fruitCount);
        }
    }
    public void GetCollectibleDiamond(int amount)
    {
        diamondCount += amount;
        if (UIController.instance != null)
        {
            UIController.instance.UpdateDiamondDisplay(diamondCount);
        }
    }
}
