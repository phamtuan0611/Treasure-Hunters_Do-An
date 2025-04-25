using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackHomeScene : MonoBehaviour
{
    [SerializeField] private GameObject[] pack;
    // Start is called before the first frame update
    void Start()
    {
        if (pack == null || pack.Length == 0)
            return;

        foreach (GameObject obj in pack)
        {
            if (obj != null)
                obj.SetActive(false);
        }

        int randomIndex = Random.Range(0, pack.Length);
        pack[randomIndex].SetActive(true);
    }
}
