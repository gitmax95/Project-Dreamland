using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    PlayerHealthSystem playerHealthScript;
    BasicMovement_Player playerMovement;
    ControllerStates controllerStates;
    GameObject playerAppearance;
    CapsuleCollider2D playerCollider;

    //PolygonCollider2D playerCollider;

    public Animator animator;

    [Header("Player States")]
    public bool isFacingRight;
    public bool isIdle;
    public bool isTouchingGround;
    public bool isRunning;
    public bool isSliding;
    public bool isJumping;
    public bool jumpActivated;
    public bool isWallJumping;
    public bool isGrounded;
    //public bool inAir;
    public bool isTouchingWall; //is Touching && IS FACING WALL!
    //public bool onWall;
    public bool isWallSliding;
    public bool isDying;
    public bool isDead;
    public bool hasSpikedShoes;  //Testing purposes

    public bool touchingLeftWall;
    public bool touchingRightWall;

    float runDuration;
    

    private void Start()
    {
        playerMovement = GameObject.Find("PlayerChar").GetComponent<BasicMovement_Player>();
        controllerStates = GameObject.Find("InputManager").GetComponent<ControllerStates>();
        playerHealthScript = GameObject.Find("PlayerChar").GetComponent<PlayerHealthSystem>();
        playerCollider = GameObject.Find("Appearance").GetComponent<CapsuleCollider2D>();
        playerAppearance = GameObject.Find("Appearance");

        isFacingRight = true;

        touchingLeftWall = false;
        touchingRightWall = false;

        isDying = false;
        isDead = false;
    }
    void Update()
    {
        IdleState();

        RunState();

        SlideState();

        JumpState();

        WallJumpState();

        GroundedState();

        WallSlideState();

        AirStrafeState();

        DyingState();

        //DeadState();
        //WallHangState();

    }

    private void SlideState()
    {
        if (isRunning && (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) || controllerStates.rightFinger == ControllerStates.FingerState.downSwipe)) //Future iteration will only use fingerState and we'll have more InputMangerScripts
            {
            if (runDuration >= playerMovement.requiredRunDuration)
            {
                isRunning = false;
                playerCollider.enabled = false;

                isSliding = true;
                animator.SetBool("isSliding", true); //Animation for Sliding
            }
        }
        else if (playerMovement.timer_slideDuration >= playerMovement.slideDuration || isJumping || isIdle /*|| !isRunning*/)
        {
            runDuration = 0.0f;
            isSliding = false;
            animator.SetBool("isSliding", false);

            playerCollider.enabled = true;
        }
    }

    private void IdleState()
    {
        if (isTouchingGround && controllerStates.input_Horizontal == 0)
        {
            isIdle = true;
            animator.SetBool("isIdle", true); //Animation for Idle
        } 
        else if (!isTouchingGround || isRunning || isSliding || isJumping || isWallSliding)
        {
            isIdle = false;
            animator.SetBool("isIdle", false);
        }
    }

    private void GroundedState()
    {
        if (!isTouchingGround && !isTouchingWall)
        {
            isGrounded = false;
            animator.SetBool("isGrounded", false);
        }
        else if (!isTouchingGround && isTouchingWall)
        {
            isGrounded = false;
            animator.SetBool("isGrounded", false);
        }
        else if (isTouchingGround)
        {
            isGrounded = true;
            animator.SetBool("isGrounded", true);
        }
    }

    private void JumpState()
    {
        if(isJumping && !isTouchingGround) {
            jumpActivated = true;
        }

        if (isGrounded && Input.GetKeyDown(KeyCode.Space) || controllerStates.rightFinger == ControllerStates.FingerState.tap) //&& playerMovement.timer_jumpDuration < playerMovement.jumpDuration
        {
            isJumping = true;
            animator.SetBool("isJumping", true);
            //isGrounded = false;
            //animator.SetBool("isGrounded", false);
        }
        else if (jumpActivated && isTouchingGround || isWallSliding)
        {
           // if (isJumping){ //playerMovement.timer_jumpDuration > 0.02f//This needs to be made in a different way otherwise the Landing becomes inconsistent.

            isJumping = false;
            animator.SetBool("isJumping", false);
            jumpActivated = false;
           
        }
        //else if (isWallSliding)
        //{
        //    animator.SetBool("isJumping", false);
        //    isJumping = false;
        //}
    }

    private void WallJumpState()
    {
        if (!isGrounded && isWallSliding && Input.GetKey(KeyCode.Space))
        {
            isWallJumping = true;
            animator.SetBool("isJumping", true);
            //playerMovement.timerWallJump += Time.deltaTime;
        }
        else if (!isJumping && isTouchingGround || isTouchingWall || isWallSliding)
        {
            isWallJumping = false;
            animator.SetBool("isJumping", false);
            //playerMovement.timerWallJump = 0.0f;
        }
    }

        private void RunState()
    {
        if (isTouchingGround && !isSliding && !isJumping && !isWallSliding && isGrounded)
        {
            if (controllerStates.input_Horizontal < -playerMovement.joystick_Threshold || controllerStates.input_Horizontal > playerMovement.joystick_Threshold)
            {
                isRunning = true;
                animator.SetBool("isRunning", true); //Animation for Running
                runDuration += Time.deltaTime;
            } else
            {
                isRunning = false;
                animator.SetBool("isRunning", false);        
            }
        }
        else if(!isTouchingGround || isJumping || isSliding || isWallSliding || isGrounded || isIdle)
        {
            animator.SetBool("isRunning", false);
            isRunning = false;
        }
    }

    private void WallSlideState()
    {
        if (isTouchingWall && !isTouchingGround && playerMovement.rigidBodyPlayer.velocity.y < 0)
        {
            if (touchingRightWall && (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)))
            {
                isWallSliding = true;
                animator.SetBool("isWallSliding", true);
                playerMovement.wallSlideSpeed = 0.1f;
            }
            else if (touchingLeftWall && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)))
            {
                isWallSliding = true;
                animator.SetBool("isWallSliding", true);
                playerMovement.wallSlideSpeed = 0.1f;
            }
            else
            {
                isWallSliding = true;
                animator.SetBool("isWallSliding", true);
                playerMovement.wallSlideSpeed = 1.0f;
            }
        }
        else
        {
            isWallSliding = false;
            animator.SetBool("isWallSliding", false);
        }
    }

    private void DyingState()
    {
        if (playerHealthScript.playerHealth <= 0)
        {
            isDying = true;
            animator.SetBool("isDying", true);
            playerAppearance.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    //private void DeadState()
    //{
    //    if (playerHealthScript.animationFinish)
    //    {
    //        isDying = false;
    //        animator.SetBool("isDying", false);
    //        isDead = true;
    //        animator.SetBool("isDead", true);
    //    }
    //}

    private void AirStrafeState()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isTouchingGround = true;
            animator.SetBool("isGrounded", true);
            
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isTouchingGround = true;
            animator.SetBool("isGrounded", true);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isTouchingGround = false;
            animator.SetBool("isGrounded", false);
        }
    }
}