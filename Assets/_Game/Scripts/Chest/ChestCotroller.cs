using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChestCotroller : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private ParticleSystem fireWork_1, fireWork_2;
    [SerializeField] private GameObject chestImage;
    public GameObject endTransitionScene;

    public Transform camPoint;
    private CameraController camController;
    public Camera theCam;
    public float cameraMoveSpeed;
    private bool isCamera;
    private void Start()
    {
        //camController = FindFirstObjectByType<CameraController>();
        camController = theCam.GetComponent<CameraController>();
        isCamera = false;

        chestImage.SetActive(false);
        endTransitionScene.SetActive(false);
    }

    private void Update()
    {
        if (isCamera)
            camController.transform.position = Vector3.MoveTowards(camController.transform.position, camPoint.position, cameraMoveSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerPrefs.SetInt("CompletedLevel", 5);
            PlayerPrefs.Save();
            
            anim.SetTrigger("isPlayer");
            AudioManager.instance.PlaySFX(AudioManager.instance.openChest);
            camController.enabled = false;
            isCamera = true;

            chestImage.SetActive(true);

            StartCoroutine(DelayParticle());
        }
    }

    IEnumerator DelayParticle()
    {
        yield return new WaitForSeconds(0.5f);

        fireWork_1.Play();
        
        yield return new WaitForSeconds(1f);
        //AudioManager.instance.PlayLoopSFX(AudioManager.instance.fireWork);
        fireWork_2.Play();
        yield return new WaitForSeconds(0.5f);
        AudioManager.instance.PlayLoopSFX(AudioManager.instance.fireWork);
        yield return new WaitForSeconds(7f);
        AudioManager.instance.StopLoopSFX();
        StartCoroutine(DelayEndTransition());
    }

    IEnumerator DelayEndTransition()
    {
        endTransitionScene.SetActive(true);

        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadSceneAsync("Victory");
    }
}
