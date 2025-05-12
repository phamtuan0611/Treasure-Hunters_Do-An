using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattleController : MonoBehaviour
{
    private bool bossActive;
    public GameObject blockers;

    public Transform camPoint;
    private CameraController camController;
    public float cameraMoveSpeed;

    private float originalCameraSize; 
    public float bossCameraSize = 7.5f;
    public float sizeChangeSpeed = 2f;

    public Transform theBoss;
    public float bossGrowSpeed;

    public Transform projectileLauncher;
    public float launcherSpeed = 2f;

    public float laucncherRotateSpeed = 90f;
    private float launcherRotation;

    public GameObject projectileToFire;
    public Transform[] projectilePoints;
    public Transform[] projectileMiniBoss;

    public float waitToStartShooting, timeBetweenShots;
    private float shootStartCounter, shotCounter;
    private int currentShoot;

    private int currentShootMiniBoss;
    private float shootCounterMini;

    public Animator bossAnim;
    private bool isWeak;

    public Transform[] bossMovePoints;
    private int currentMovePoint;
    public float bossMoveSpeed;

    private int currentPhase;

    public GameObject deathEffect; 

    public GameObject player;


    public Transform[] miniBoss, theTraps;

    // Start is called before the first frame update
    void Start()
    {
        //camController = FindFirstObjectByType<CameraController>();
        camController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        originalCameraSize = camController.GetComponent<Camera>().orthographicSize;

        shootStartCounter = waitToStartShooting;

        foreach (Transform tt in theTraps)
        {
            tt.gameObject.SetActive(false);
        }

        blockers.transform.SetParent(null);
    }

    // Update is called once per frame
    void Update()
    {
        if (bossActive == true)
        {
            camController.transform.position = Vector3.MoveTowards(camController.transform.position, camPoint.position, cameraMoveSpeed * Time.deltaTime);
            Camera mainCam = camController.GetComponent<Camera>();
            if (mainCam.orthographicSize != bossCameraSize)
            {
                mainCam.orthographicSize = Mathf.MoveTowards(mainCam.orthographicSize, bossCameraSize, sizeChangeSpeed * Time.deltaTime);
            }

            foreach (Transform tt in theTraps)
            {
                tt.gameObject.SetActive(true);
            }

            if (theBoss.localScale != Vector3.one)
            {
                //Hien Boss
                theBoss.localScale = Vector3.MoveTowards(theBoss.localScale, Vector3.one, bossGrowSpeed * Time.deltaTime);

            }
              
            if (currentPhase == 3)
            {
                foreach (Transform bm in miniBoss)
                {
                    bm.gameObject.SetActive(true);
                    bm.localScale = Vector3.MoveTowards(bm.localScale, new Vector3(0.5f, 0.5f, 0f), (bossGrowSpeed / 2f) * Time.deltaTime);
                }
            }
            //theBoss.localScale == Vector3.one && 
            if (projectileLauncher.localScale != Vector3.one)
            {
                //Hien Dan 
                projectileLauncher.localScale = Vector3.MoveTowards(projectileLauncher.localScale, Vector3.one, bossGrowSpeed * Time.deltaTime);
            }

            launcherRotation += laucncherRotateSpeed * Time.deltaTime;
            if (launcherRotation > 360) launcherRotation -= 360f;
            projectileLauncher.localRotation = Quaternion.Euler(0f, 0f, launcherRotation);

            //start shooting
            if (shootStartCounter > 0f)
            {
                shootStartCounter -= Time.deltaTime;
                if (shootStartCounter <= 0f)
                {
                    shotCounter = timeBetweenShots;

                    FireShoot();
                }
            }

            if (shotCounter > 0f)
            {
                shotCounter -= Time.deltaTime;

                if (shotCounter <= 0f)
                {
                    shotCounter = timeBetweenShots;

                    FireShoot();
                }

                if (currentPhase == 3)
                {
                    shootCounterMini -= Time.deltaTime;
                    if (shootCounterMini <= 0f)
                    {
                        shootCounterMini = timeBetweenShots;
                        miniBossFire();
                    }
                }
            }

            if (isWeak == false && shootStartCounter <= 1f)
            {
                theBoss.transform.position = Vector3.MoveTowards(theBoss.transform.position, bossMovePoints[currentMovePoint].position, bossMoveSpeed * Time.deltaTime);

                if (theBoss.transform.position == bossMovePoints[currentMovePoint].position)
                {
                    currentMovePoint++;

                    if (currentMovePoint >= bossMovePoints.Length)
                    {
                        currentMovePoint = 0;
                    }
                }
            }

            if (isWeak == true && currentPhase == 3)
            {
                Debug.Log("miniBoss");
                bossMiniDisable();
            }
        }
    }
    public void AcitvateBattle()
    {
        bossActive = true;
        blockers.SetActive(true);
        camController.enabled = false;

        //AudioManager.instance.bossMusicPlay();
    }

    void FireShoot()
    {
        Instantiate(projectileToFire, projectilePoints[currentShoot].position, projectilePoints[currentShoot].rotation);

        projectilePoints[currentShoot].gameObject.SetActive(false);
        currentShoot++;

        if (currentShoot >= projectilePoints.Length)
        {
            shotCounter = 0f;

            MakeWeak();
        }

        //AudioManager.instance.allSFXPlay(2);
    }

    void miniBossFire()
    {
        Instantiate(projectileToFire, projectileMiniBoss[currentShootMiniBoss].position, projectileMiniBoss[currentShootMiniBoss].rotation);

        projectileMiniBoss[currentShootMiniBoss].gameObject.SetActive(false);
        currentShootMiniBoss++;

        if (currentShootMiniBoss >= projectileMiniBoss.Length)
        {
            shootCounterMini = 0f;
        }
    }

    void MakeWeak()
    {
        bossAnim.SetTrigger("isWeak");
        isWeak = true;
        //Debug.Log(currentPhase);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (isWeak == false)
            {
                PlayerHealthController.instance.DamagePLayer();
            }
            else
            {
                if (other.transform.position.y > theBoss.position.y)
                {
                    bossAnim.SetTrigger("Hit");
                    FindAnyObjectByType<PlayerController>().Jump();
                    //blueBirdSpawned = false;

                    MoveToNextPhase();
                }
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Sword"))
        {
            if (isWeak == true)
            {
                if (AttackArea.instance != null)
                {
                    Debug.Log("Lmao"); 
                    if (AttackArea.instance.attack)
                    {
                        Debug.Log("Attack Area Boss");
                        bossAnim.SetTrigger("Hit");
                        MoveToNextPhase();
                        //AudioManager.instance.allSFXPlay(6);
                    }
                }

                if (SwordController.instance != null)
                {
                    if (SwordController.instance.isAttack)
                    {
                        Debug.Log("Sword Controller Boss");
                        bossAnim.SetTrigger("Hit");
                        MoveToNextPhase();
                        //AudioManager.instance.allSFXPlay(6);
                    }
                }
            }
        }
    }
    void bossMiniDisable()
    {
        Debug.Log("Mini Boss destroy");
        foreach (Transform bm in miniBoss)
        {
            bm.localScale = Vector3.MoveTowards(bm.localScale, Vector3.zero, bossGrowSpeed * Time.deltaTime);
            bm.gameObject.SetActive(false);
        }
    }

    void MoveToNextPhase()
    {
        currentPhase++;

        if (currentPhase >= 4)
        {
            gameObject.SetActive(false);

            foreach (Transform tt in theTraps)
            {
                tt.gameObject.SetActive(false);
            }

            blockers.SetActive(false);

            camController.enabled = true;
            Camera mainCam = camController.GetComponent<Camera>();
            
            mainCam.orthographicSize = originalCameraSize;

            Instantiate(deathEffect, theBoss.position, Quaternion.identity);

            //AudioManager.instance.allSFXPlay(0);

            //AudioManager.instance.levelTracksPlay(FindFirstObjectByType<LevelMusicPlayer>().trackToPlay);
        }
        else if (currentPhase >= 2)
        {
            isWeak = false;

            //waitToStartShooting *= 0.5f;
            //timeBetweenShots *= 0.75f;
            //bossMoveSpeed *= 1.5f;

            shootStartCounter = waitToStartShooting;

            projectileLauncher.localScale = Vector3.zero;

            foreach (Transform point in projectilePoints)
            {
                point.gameObject.SetActive(true);
            }

            currentShoot = 0;

            //AudioManager.instance.allSFXPlay(1);

            //Debug.Log("Mang 3");
        }
        else if (currentPhase >= 0)
        {
            isWeak = false;

            waitToStartShooting *= 0.5f;
            timeBetweenShots *= 0.75f;
            bossMoveSpeed *= 1.5f;

            shootStartCounter = waitToStartShooting;

            projectileLauncher.localScale = Vector3.zero;

            foreach (Transform point in projectilePoints)
            {
                point.gameObject.SetActive(true);
            }

            currentShoot = 0;

            //AudioManager.instance.allSFXPlay(1);
        }
    }
}
