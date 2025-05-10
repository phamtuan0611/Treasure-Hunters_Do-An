using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateLevel : MonoBehaviour
{
    public WayPoint targetWaypoint;

    public void TriggerMove()
    {
        PlayerMovement.instance.MoveTo(targetWaypoint);
    }
}
