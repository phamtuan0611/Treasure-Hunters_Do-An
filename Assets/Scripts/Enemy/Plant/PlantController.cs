using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantController : EnemyController
{
    // Update is called once per frame
    public override void Update()
    {
        if (isDefeated == true)
        {
            waitToDestroy -= Time.deltaTime;

            if (waitToDestroy <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
