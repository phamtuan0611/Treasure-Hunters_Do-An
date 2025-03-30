using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    [SerializeField] private CheckPoint[] allCP;

    private CheckPoint activeCP;

    public Vector3 respawnPosition;
    // Start is called before the first frame update
    void Start()
    {
        allCP = FindObjectsByType<CheckPoint>(FindObjectsSortMode.None);

        foreach (CheckPoint cp in allCP)
        {
            cp.cpMan = this;
        }

        respawnPosition = FindFirstObjectByType<PlayerController>().transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            DeactivateAllCheckpoint();
        }
    }

    public void DeactivateAllCheckpoint()
    {
        foreach (CheckPoint cp in allCP)
        {
            cp.DeactivateCheckpoint();
        }
    }

    public void SetActivateCheckPoint(CheckPoint newActiveCP)
    {
        DeactivateAllCheckpoint();
        activeCP = newActiveCP;

        respawnPosition = newActiveCP.transform.position;
    }
}
