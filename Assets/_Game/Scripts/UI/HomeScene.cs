using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeScene : MonoBehaviour
{
    public static HomeScene instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public bool isSetting;

    // Start is called before the first frame update
    void Start()
    {
        isSetting = false;
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

    public void ButtonSettingOpen()
    {
        isSetting = true;
    }
    public void ButtonSettingClose()
    {
        isSetting = false;
    }

    public void ButtonQuit()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }
}
