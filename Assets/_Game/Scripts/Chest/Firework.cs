using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firework : MonoBehaviour
{
    void OnEnable()
    {
        if (AudioManager.instance != null && AudioManager.instance.fireWork != null)
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.fireWork);
        }
    }
}
