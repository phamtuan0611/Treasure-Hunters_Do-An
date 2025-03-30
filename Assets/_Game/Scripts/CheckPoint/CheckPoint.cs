using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private Animator anim;
    private bool isActive;

    [HideInInspector]
    public CheckPointManager cpMan;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && isActive == false)
        {
            cpMan.SetActivateCheckPoint(this);

            anim.SetBool("flagActive", true);
            isActive = true;

            //AudioManager.instance.allSFXPlay(3);
        }
    }

    public void DeactivateCheckpoint()
    {
        anim.SetBool("flagActive", false);
        isActive = false;
    }
}
