using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    public static ShopController instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public bool isBuying;
    // Start is called before the first frame update
    void Start()
    {
        isBuying = false;
    }

    public void BuyingInShop()
    {
        isBuying = true;
    }

    public void NonBuyingInShop()
    {
        isBuying = false;
    }
}
