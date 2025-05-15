using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [Header("---------- Audio Source ----------")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("---------- Audio ----------")]
    public AudioClip bookScene;
    public AudioClip homeScene;
    public AudioClip selectScene;
    public AudioClip level0, level1, level2, level3, level4, level5;
    public AudioClip bossBattle;
    public AudioClip victory;

    [Header("---------- SFX ----------")]
    public AudioClip playerJump;
    public AudioClip playerAttack, playerThrowSword, playerHurt, playerDead;
    public AudioClip enemyHit, bossHit, bossDeath, bossShot;
    public AudioClip button, btnBuyDone, btnCantBuy;
    public AudioClip checkPoint, checkEnd, loseLevel;
    public AudioClip openGate, openChest, fireWork;
    public AudioClip pickUp, usePotion, breakBox;
    public AudioClip spikeHead, trampoline;
    public AudioClip flipPaper;


    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Tùy theo tên scene mà đổi nhạc
        switch (scene.name)
        {
            case "BookScene":
                PlayMusic(bookScene);
                break;
            case "HomeScene":
                PlayMusic(homeScene);
                break;
            case "LevelSelect":
                PlayMusic(selectScene);
                break;
            case "Level 0":
                PlayMusic(level0);
                break;
            case "Level 1":
                PlayMusic(level1);
                break;
            case "Level 2":
                PlayMusic(level2);
                break;
            case "Level 3":
                PlayMusic(level3);
                break;
            case "Level 4":
                PlayMusic(level4);
                break;
            case "Level 5":
                PlayMusic(level5);
                break;
            case "Victory":
                PlayMusic(victory);
                break;
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        if (musicSource.clip == clip) return; // Tránh phát lại nếu đang phát rồi
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}
