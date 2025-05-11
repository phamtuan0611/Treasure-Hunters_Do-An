using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateLevel : MonoBehaviour
{
    public WayPoint targetWaypoint;
    public string nameLevel;

    public void TriggerMove()
    {
        PlayerMovement.instance.MoveTo(targetWaypoint);
    }
}
