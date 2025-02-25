using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    public Rigidbody2D theRB;
    [SerializeField] private float jumpForce;
    [SerializeField] private float runSpeed;
    private float activeSpeed;

    private bool isGrounded;

    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask whatIsGround;
    private bool canDoubleJump;

    [SerializeField] private Animator anim;

    private int countAttack = 0;
    private int countAirAttack = 0;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Check Ground
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, whatIsGround);

        //Run
        activeSpeed = moveSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            activeSpeed = runSpeed;
        }

        //Move
        theRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * activeSpeed, theRB.velocity.y);

        //Jump and Double Jump
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded == true)
            {
                Jump();
                canDoubleJump = true;
                anim.SetBool("isDoubleJump", false);
            }
            else if (canDoubleJump == true)
            {
                Jump();
                canDoubleJump = false;
                //anim.SetBool("isDoubleJumping", true);
                anim.SetTrigger("isDoubleJump");
            }
        }

        //Attack
        if (Input.GetKeyDown(KeyCode.Z))
        {
            countAttack++;
            if (countAttack == 1)
            {
                anim.SetTrigger("attack01");
            }
            else if (countAttack == 2)
            {
                anim.SetTrigger("attack02");
            }
            else if (countAttack == 3)
            {
                anim.SetTrigger("attack03");

                countAttack = 0;
            }
        }

        //Air Attack
        if (Input.GetKeyDown(KeyCode.X) && isGrounded == false)
        {
            countAirAttack++;
            if (countAirAttack == 1)
            {
                anim.SetTrigger("airAttack01");
            }
            else
            {
                anim.SetTrigger("airAttack02");

                countAirAttack = 0;
            }

        }

        //Throw Sword
        if (Input.GetKeyDown(KeyCode.C))
        {
            anim.SetTrigger("throwSword");
        }

        //ChangeDirection
        if (theRB.velocity.x >= 0)
        {
            transform.localScale = Vector3.one;
        }
        if (theRB.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        anim.SetFloat("speed", Mathf.Abs(theRB.velocity.x));
        anim.SetBool("isGround", isGrounded);
        anim.SetFloat("ySpeed", theRB.velocity.y);

    }

    public void Jump()
    {
        theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
    }
}
