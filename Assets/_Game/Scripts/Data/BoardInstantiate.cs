using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardInstantiate : MonoBehaviour
{
    private BoardPotion boardPotion;

    private void Start()
    {
        boardPotion = GetComponent<BoardPotion>();
    }

    public void InstanPotion(GameObject potion)
    {
        if (boardPotion != null && boardPotion.isPotion == true)
        {
            if (potion.name == "HealthPickup")
            {
                if (PlayerHealthController.instance.currentHealth == PlayerHealthController.instance.maxHealth)
                    return;
                else
                    Instantiate(potion, PlayerHealthController.instance.transform.position, Quaternion.identity);
            }
            else
                Instantiate(potion, PlayerHealthController.instance.transform.position, Quaternion.identity);
        }
    }
}
