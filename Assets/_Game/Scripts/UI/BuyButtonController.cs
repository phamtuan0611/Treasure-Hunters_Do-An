using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyButtonController : MonoBehaviour
{
    public Sprite topIconDiamond;
    [TextArea] public string descriptionDiamond;

    public Sprite topIconFruit;
    [TextArea] public string descriptionFruit;

    public string btn1Text;

    public string btn2Text;

    public ShopPopup shopPopup;

    public void OnClickBuy()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.button);
        shopPopup.SetupPopup(topIconDiamond, descriptionDiamond, topIconFruit, descriptionFruit, btn1Text, btn2Text);

        ShopController.instance.BuyingInShop();
    }
}
