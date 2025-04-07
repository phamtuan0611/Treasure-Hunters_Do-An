using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBirdController : MonoBehaviour
{
    [SerializeField] private bool isAttack;
    [SerializeField] public GameObject blueBrid, thePlayer, patrolPoint;
    [SerializeField] private float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        patrolPoint.transform.SetParent(null);
        isAttack = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttack == true)
        {
            blueBrid.transform.position = Vector3.MoveTowards(blueBrid.transform.position, thePlayer.transform.position, speed * Time.deltaTime);
            if (blueBrid.transform.position.x > patrolPoint.transform.position.x)
            {
                blueBrid.transform.localScale = new Vector3(-1f, 1f, 1f);
            }else if (blueBrid.transform.position.x < patrolPoint.transform.position.x)
            {
                blueBrid.transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }

        if (isAttack == false)
        {
            blueBrid.transform.position = Vector3.MoveTowards(blueBrid.transform.position, patrolPoint.transform.position, speed * Time.deltaTime);
            if (blueBrid.transform.position.x > patrolPoint.transform.position.x)
            {
                blueBrid.transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else if (blueBrid.transform.position.x < patrolPoint.transform.position.x)
            {
                blueBrid.transform.localScale = new Vector3(-1f, 1f, 1f);
            }
        }   
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isAttack = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isAttack = false;
        }
    }
}
