using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackGround : MonoBehaviour
{
    public static ParallaxBackGround instance;

    private void Awake()
    {
        instance = this;
    }

    private Transform theCam;
    [SerializeField] private Transform sky, treeline;

    [Range(0f, 1f)]
    [SerializeField] private float parallaxSpeed;

    // Start is called before the first frame update
    void Start()
    {
        theCam = Camera.main.transform;
    }

    public void MoveBackGround()
    {
        sky.position = new Vector3(theCam.position.x, theCam.position.y, sky.position.z);

        treeline.position = new Vector3(theCam.position.x * parallaxSpeed, theCam.position.y * parallaxSpeed, treeline.position.z);
    }
}
