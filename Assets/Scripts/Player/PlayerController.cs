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

    private int changePhase;

    [SerializeField] private float knockbackLength, knockbackSpeed;
    private float knockbackCounter;

    // Start is called before the first frame update
    void Start()
    {
        changePhase = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale > 0f)
        {
            //Check Ground
            isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, whatIsGround);

            if (knockbackCounter <= 0)
            {
                //Change Phase
                if (Input.GetKeyDown(KeyCode.F))
                {
                    changePhase++;
                    if (changePhase == 1)
                    {
                        anim.SetFloat("ChangePhase", 1);
                    }
                    else if (changePhase == 2)
                    {
                        anim.SetFloat("ChangePhase", 0);
                        changePhase = 0;
                    }

                }
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
                        anim.SetTrigger("isDoubleJump");
                    }
                }

                //Attack
                if (Input.GetKeyDown(KeyCode.Z) && changePhase == 1)
                {
                    countAttack++;
                    anim.SetBool("ATTACK", true);
                    if (countAttack == 1)
                    {
                        anim.SetFloat("attack", 0);
                    }
                    else if (countAttack == 2)
                    {
                        anim.SetFloat("attack", 0.5f);
                    }
                    else if (countAttack == 3)
                    {
                        anim.SetFloat("attack", 1f);

                        countAttack = 0;
                    }

                }
                else
                {
                    anim.SetBool("ATTACK", false);
                }

                //Air Attack
                if (Input.GetKeyDown(KeyCode.X) && isGrounded == false && changePhase == 1)
                {
                    countAirAttack++;
                    anim.SetBool("AIRATTACK", true);
                    if (countAirAttack == 1)
                    {
                        anim.SetFloat("airAttack", 0);
                    }
                    else if (countAirAttack == 2)
                    {
                        anim.SetFloat("airAttack", 1);

                        countAirAttack = 0;
                    }
                }
                else
                {
                    anim.SetBool("AIRATTACK", false);
                }

                //Throw Sword
                if (Input.GetKeyDown(KeyCode.C))
                {
                    anim.SetTrigger("throwSword");
                }

                //ChangeDirection
                if (theRB.velocity.x > 0)
                {
                    transform.localScale = Vector3.one;
                }
                if (theRB.velocity.x < 0)
                {
                    transform.localScale = new Vector3(-1f, 1f, 1f);
                }
            }
            else
            {
                knockbackCounter -= Time.deltaTime;
                theRB.velocity = new Vector2(knockbackSpeed * -transform.localScale.x, theRB.velocity.y);
            }

            anim.SetFloat("speed", Mathf.Abs(theRB.velocity.x));
            anim.SetBool("isGround", isGrounded);
            anim.SetFloat("ySpeed", theRB.velocity.y);

            
        }
    }

    public void Jump()
    {
        theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
    }

    public void isKnock()
    {
        theRB.velocity = new Vector2(0f, jumpForce * 0.65f);
        anim.SetTrigger("hit");
        knockbackCounter = knockbackLength;
    }

    public void isDead()
    {
        theRB.velocity = new Vector2(0f, jumpForce * 0.65f);
        anim.SetTrigger("dead");
        knockbackCounter = knockbackLength;
    }
}
