using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private bool freezeVertical, freezeHorizontal;
    private Vector3 positionStore;

    [SerializeField] private bool clampPosition;
    [SerializeField] private Transform clampMin, clampMax;
    private float halfWidth, halfHeight;
    [SerializeField] private Camera theCam;
    // Start is called before the first frame update
    void Start()
    {
        //gan vi tri ban dau cua Camera
        positionStore = transform.position;
        clampMin.SetParent(null); clampMax.SetParent(null); //Boi vi ca 2 la child cua Camera, neu khong setParent(null) thi x hoac y cua Camera luon trong pham vi cho phep

        //Duoc dung de gioi han player trong man hinh (Khong hieu cho lam :))) )
        halfHeight = theCam.orthographicSize;
        halfWidth = theCam.orthographicSize * theCam.aspect;
    }

    // Update is called once per frame
    //Su dung LateUpdate thay cho Update vi trong Unity, doi khi Update cua Camera se duoc goi truoc Update cua Player, dan toi hien tuong giat.
    //Vi the su dung LateUpdate de dam bao chac chan la Camera se duoc goi sau Player
    void LateUpdate()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);

        if (freezeVertical == true)
        {
            //Lay vi tri y ban dau, con x va z se theo target
            transform.position = new Vector3(target.position.x, positionStore.y, transform.position.z);
        }
        if (freezeHorizontal == true)
        {
            //Lay vi tri x ban dau, con y va z se theo target
            transform.position = new Vector3(positionStore.x, target.position.y, transform.position.z);
        }

        if (clampPosition == true)
        {
            //Check neu x cua Camera < x cua clampMin thi transform.position.x = clampMin.position.x
            //Tuong tu voi clampMax va y
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, clampMin.position.x + halfWidth, clampMax.position.x - halfWidth),
                Mathf.Clamp(transform.position.y, clampMin.position.y + halfHeight, clampMax.position.y - halfHeight),
                transform.position.z);
        }

        if (ParallaxBackGround.instance != null)
        {
            ParallaxBackGround.instance.MoveBackGround();
        }
    }

    //Ve khu vuc gioi han cua Camera
    private void OnDrawGizmos()
    {
        if (clampPosition == true)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(clampMin.position, new Vector3(clampMin.position.x, clampMax.position.y, 0f));
            Gizmos.DrawLine(clampMin.position, new Vector3(clampMax.position.x, clampMin.position.y, 0f));

            Gizmos.DrawLine(clampMax.position, new Vector3(clampMin.position.x, clampMax.position.y, 0f));
            Gizmos.DrawLine(clampMax.position, new Vector3(clampMax.position.x, clampMin.position.y, 0f));
        }
    }
}
