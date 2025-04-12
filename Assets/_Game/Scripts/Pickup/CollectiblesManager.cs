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

    // Start is called before the first frame update
    void Start()
    {
        //collectibleCount = InfoTracker.instance.currentFruits;

        if (UIController.instance != null)
        {
            //UIController.instance.UpdateCollectibleDisplay(collectibleCount);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetCollectibleFruit(int amount)
    {
        fruitCount += amount;
        if (UIController.instance != null)
        {
            UIController.instance.UpdateFruitDisplay(fruitCount);
        }

        //if (collectibleCount >= extraLifeThreshold)
        //{
        //    collectibleCount -= extraLifeThreshold;
        //    if (LifeController.instance != null)
        //    {
        //        LifeController.instance.AddLife();
        //    }
        //}
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
