using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    BasicMovement_Player playerMovement;

    PolygonCollider2D playerCollider;

    public Animator animator;

    [Header("Player States")]
    public bool isFacingRight;
    public bool isIdle;
    public bool isTouchingGround;
    public bool isRunning;
    public bool isSliding;
    public bool isJumping;
    public bool isWallJumping;
    public bool isGrounded;
    //public bool inAir;
    public bool isTouchingWall; //is Touching && IS FACING WALL!
    //public bool onWall;
    public bool isWallSliding;
    public bool hasSpikedShoes;  //Testing purposes

    public bool touchingLeftWall;
    public bool touchingRightWall;

    float runDuration;

    private void Start()
    {
        playerMovement = GameObject.Find("PlayerChar").GetComponent<BasicMovement_Player>();
        isFacingRight = true;

        touchingLeftWall = false;
        touchingRightWall = false;
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

        //WallHangState();

    }

    private void SlideState()
    {
        if (isRunning && (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)))
            {
            if (runDuration >= playerMovement.requiredRunDuration)
            {
                isRunning = false;
                isSliding = true;
                animator.SetBool("isSliding", true); //Animation for Sliding
            }
        }
        else if (playerMovement.timer_slideDuration >= playerMovement.slideDuration || isJumping)
        {
            runDuration = 0.0f;
            isSliding = false;
            animator.SetBool("isSliding", false);
        }
    }

    private void IdleState()
    {
        if (isTouchingGround && Input.GetAxis("Horizontal") == 0)
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
        if (isGrounded && Input.GetKey(KeyCode.Space) && playerMovement.timer_jumpDuration < playerMovement.jumpDuration)
        {
            isJumping = true;
            animator.SetBool("isJumping", true);
            //isGrounded = false;
            //animator.SetBool("isGrounded", false);
        }
        else if (isTouchingGround || isWallSliding)
        {
            isJumping = false;
            animator.SetBool("isJumping", false);
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
        }
        else if (isTouchingGround || isWallSliding)
        {
            isWallJumping = false;
            animator.SetBool("isJumping", false);
        }
    }

        private void RunState()
    {
        if (isTouchingGround && !isSliding && !isJumping && !isWallSliding && isGrounded)
        {
            if (Input.GetAxis("Horizontal") < -playerMovement.joystick_Threshold || Input.GetAxis("Horizontal") > playerMovement.joystick_Threshold)
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
