using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ButtonPlay()
    {
        Debug.Log("Play");
    }

    public void ButtonShop()
    {
        Debug.Log("Shop");
    }

    public void ButtonSetting()
    {
        Debug.Log("Setting");
    }

    public void ButtonQuit()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }
}
