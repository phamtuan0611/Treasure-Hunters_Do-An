using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    public Rigidbody2D theRB;
    [SerializeField] private float jumpForce;
    [SerializeField] private float normalSpeed, normalJump;
    public float activeSpeed;

    private bool isGrounded;
    //private bool wasFalling = false;

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

    [SerializeField] private GameObject effectPlayerJump, effectPlayerFall, effectPlayerRun;
    private bool checkFall, checkRun;

    [SerializeField] public GameObject attackArea;
    private Collider2D attackCollider;

    private bool isAttacking = false;

    [SerializeField] private GameObject swordPrefab;
    [SerializeField] public float throwForce = 8f;
    private float throwTimer = 0f;

    private float timeChangePhase;

    public float timePotionSpeed, timePotionJump;
    private bool potionActiveSpeed, potionActiveJump, highSpeedPotion, highJumpPotion;

    private float timeDiamondPotion;
    public bool diamondPotion;

    private void Awake()
    {
        attackCollider = attackArea.GetComponent<Collider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        changePhase = 0;
        timeChangePhase = 2f;

        throwTimer = 2f;

        timePotionSpeed = 10f;
        timePotionJump = 10f;
        potionActiveSpeed = false;
        potionActiveJump = false;

        highSpeedPotion = false; highJumpPotion = false;

        timeDiamondPotion = 10f;
        diamondPotion = false;

        attackCollider.enabled = false;

        normalSpeed = moveSpeed;
        normalJump = jumpForce;

        checkFall = false;
        checkRun = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale < 0f)
            return;
        if (Time.timeScale > 0f)
        {
            //Check Ground
            isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, whatIsGround);

            throwTimer += Time.deltaTime;
            timeChangePhase += Time.deltaTime;

            if (knockbackCounter <= 0)
            {
                //Change Phase
                ChangePhase();

                //Run
                activeSpeed = moveSpeed;
                //if (Input.GetKey(KeyCode.LeftShift))
                //{
                //    activeSpeed = normalSpeed;
                //}

                //Move
                theRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * activeSpeed, theRB.velocity.y);

                //Jump and Double Jump
                PlayerJump();

                //PLayer Attack
                PlayerAttack();

                //ChangeDirection
                ChangeDirection();
            }
            else
            {
                knockbackCounter -= Time.deltaTime;
                theRB.velocity = new Vector2(knockbackSpeed * -transform.localScale.x, theRB.velocity.y);
            }

            //Effect Run
            if (Mathf.Abs(theRB.velocity.x) != 0)
            {
                effectPlayerRun.SetActive(true);
                
                checkRun = false;
            }
            if (checkRun == false && (Mathf.Abs(theRB.velocity.x) == 0 || isGrounded == false))
            {
                effectPlayerRun.SetActive(false);
                checkRun = true;
            }

            anim.SetFloat("speed", Mathf.Abs(theRB.velocity.x));
            anim.SetBool("isGround", isGrounded);
            anim.SetFloat("ySpeed", theRB.velocity.y);

            if (potionActiveSpeed == true)
            {
                if (highSpeedPotion == true)
                {
                    UIController.instance.UpdateHighSpeed(timePotionSpeed);

                    timePotionSpeed -= Time.deltaTime;
                    if (timePotionSpeed <= 0)
                    {
                        potionActiveSpeed = false;
                        highSpeedPotion = false;

                        moveSpeed = normalSpeed;

                        UIController.instance.highSpeed.SetActive(false);
                    }
                }
            }

            if (potionActiveJump == true)
            {
                if (highJumpPotion == true)
                {
                    UIController.instance.UpdateLowSpeed(timePotionJump);

                    timePotionJump -= Time.deltaTime;
                    if (timePotionJump <= 0)
                    {
                        potionActiveJump = false;
                        highJumpPotion = false;

                        jumpForce = normalJump;

                        UIController.instance.lowSpeed.SetActive(false);
                    }
                }
            }

            if (diamondPotion == true)
            {
                UIController.instance.UpdateMultiplyScore(timeDiamondPotion);
                timeDiamondPotion -= Time.deltaTime;
                if (timeDiamondPotion <= 0)
                {
                    diamondPotion = false;
                    UIController.instance.multiplyScore.SetActive(false);
                }
            }

            //Effect Fall
            if (theRB.velocity.y == 0 && checkFall == true)
            {
                GameObject effectFall = Instantiate(effectPlayerFall, new Vector3(transform.position.x, transform.position.y - 0.35f, transform.position.z), Quaternion.identity);
                Destroy(effectFall, 0.3f);

                checkFall = false;
            }
        }
    }

    public void ChangePhase()
    {
        if (Input.GetKeyDown(KeyCode.F) && timeChangePhase >= 2f)
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
    }
    public void Jump()
    {
        theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
    }

    public void PlayerJump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded == true)
            {
                Jump();
                canDoubleJump = true;
                anim.SetBool("isDoubleJump", false);
                
                SpawnEffect();
                checkFall = true;
            }
            else if (canDoubleJump == true)
            {
                Jump();
                canDoubleJump = false;
                anim.SetTrigger("isDoubleJump");
                
                SpawnEffect();
            }
        }
    }

    public void PlayerAttack()
    {
        //Attack
        if (Input.GetKeyDown(KeyCode.Z) && changePhase == 1 && !isAttacking)
        {
            isAttacking = true;
            countAttack++;
            anim.SetBool("ATTACK", true);

            attackCollider.enabled = true;

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

            StartCoroutine(AttackDisable());
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
        if (Input.GetKeyDown(KeyCode.C) && throwTimer >= 2f && anim.GetFloat("ChangePhase") != 0)
        {
            anim.SetTrigger("throwSword");
            StartCoroutine(ThrowSword());
        }
    }

    public IEnumerator ThrowSword()
    {
        GameObject thrownSword = Instantiate(swordPrefab, transform.position, Quaternion.identity);
        anim.SetFloat("ChangePhase", 0);
        Rigidbody2D rb = thrownSword.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            float direction = transform.localScale.x;
            rb.velocity = new Vector2(direction * throwForce, 0f);

            if (direction > 0)
            {
                thrownSword.transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else
            {
                thrownSword.transform.localScale = new Vector3(-1f, 1f, 1f);
            }
        }

        Destroy(thrownSword, 2f);
        throwTimer = 0f;
        timeChangePhase = 0f;
        changePhase = 0;

        yield return new WaitForSeconds(2f);
        anim.SetFloat("ChangePhase", 1);
        changePhase = 1;
    }

    public void ChangeDirection()
    {
        if (theRB.velocity.x > 0)
        {
            transform.localScale = Vector3.one;
        }
        if (theRB.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        //if (theRB.velocity.y < -0.1f && !isGrounded)
        //{
        //    wasFalling = true;
        //}
    }

    public void isKnock()
    {
        theRB.velocity = new Vector2(0f, jumpForce * 0.6f);
        anim.SetTrigger("hit");
        knockbackCounter = knockbackLength;
    }

    public void isDead()
    {
        theRB.velocity = new Vector2(0f, jumpForce * 0.6f);
        anim.SetTrigger("dead");
        knockbackCounter = knockbackLength;
    }

    //Effect Jump
    public void SpawnEffect()
    {
        GameObject effectJump = Instantiate(effectPlayerJump, new Vector3(transform.position.x, transform.position.y - 0.35f, transform.position.z), Quaternion.identity);
        Destroy(effectJump, 0.3f);
    }
    private IEnumerator AttackDisable()
    {
        yield return new WaitForSeconds(0.1f);
        attackCollider.enabled = false;
        anim.SetBool("ATTACK", false);
        isAttacking = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("HighSpeedPotion"))
        {
            timePotionSpeed = 10f;

            float highSpeed = moveSpeed;
            moveSpeed = highSpeed * 2f;

            potionActiveSpeed = true;
            highSpeedPotion = true;
        }

        if (other.CompareTag("HighJumpPotion"))
        {
            timePotionJump = 10f;

            float highJump = jumpForce;
            jumpForce = highJump * 1.5f;

            potionActiveJump = true;
            highJumpPotion = true;
        }

        if (other.CompareTag("DiamondPotion"))
        {
            diamondPotion = true;
        }
    }
}
