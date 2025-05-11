using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public static LevelSelect instance;
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

    public bool isLevelPopup;

    private void Start()
    {
        isLevelPopup = false;
    }

    public void CloseLevelPopup()
    {
        isLevelPopup = false;
    }

    public void LoadHomeScene()
    {
        SceneManager.LoadScene("HomeScene");
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(LevelPopup.instance.textBoard.text);
    }
}
