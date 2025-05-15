using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private bool isMusicOn = true;
    private bool isSFXOn = true;
    void Awake()
    {
        if (instance == null)
        {
            SetupAudioManager();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void SetupAudioManager()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    [Header("---------- Audio Source ----------")]
    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioSource loopSFXSource;

    [Header("---------- Audio ----------")]
    public AudioClip bookScene;
    public AudioClip homeScene;
    public AudioClip selectScene;
    public AudioClip level0, level1, level2, level3, level4, level5;
    public AudioClip bossBattle;
    public AudioClip victory;

    [Header("---------- SFX ----------")]
    public AudioClip playerJump;
    public AudioClip playerRun, playerFall, playerAttack, playerThrowSword, playerHurt, playerDead;
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
        OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
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
        if (!isMusicOn) return;
        if (musicSource.clip == clip) return; 
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (!isSFXOn) return;
        sfxSource.PlayOneShot(clip);
    }

    public void PlayLoopSFX(AudioClip clip)
    {
        if (!isSFXOn) return;
        loopSFXSource.clip = clip;
        loopSFXSource.loop = true;
        loopSFXSource.Play();
    }

    public void StopLoopSFX()
    {
        loopSFXSource.Stop();
    }

    public void SetMusic(bool on)
    {
        isMusicOn = on;
        if (!isMusicOn)
            musicSource.Stop();
        else if (musicSource.clip != null)
            musicSource.Play();
    }

    public void SetSFX(bool on)
    {
        isSFXOn = on;
    }

    public bool GetMusicStatus()
    {
        return isMusicOn;
    }

    public bool GetSFXStatus()
    {
        return isSFXOn;
    }
}
