using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateLevel : MonoBehaviour
{
    public WayPoint targetWaypoint;
    public string nameLevel;
    public GameObject image;

    public int index;
    private void Start()
    {
        image.SetActive(false);
    }

    private void Update()
    {
        if (PlayerMovement.instance.isGate)
            image.SetActive(true);
        else
            image.SetActive(false);
    }
    public void TriggerMove()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.button);
        PlayerMovement.instance.MoveTo(targetWaypoint);
    }
}
