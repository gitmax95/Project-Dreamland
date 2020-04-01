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
    public bool onGround;
    public bool isRunning;
    public bool isSliding;
    public bool isJumping;  
    public bool inAir;
    public bool isTouchingWall;
    public bool onWall;
    public bool isWallSliding;
    public bool hasSpikedShoes;  //Testing purposes

    float runDuration;

    private void Start()
    {
        playerMovement = GameObject.Find("Player").GetComponent<BasicMovement_Player>();
        isFacingRight = true;
    }
    void Update()
    {
        CheckMovementDirection();

        IdleState();

        RunState();

        SlideState();

        JumpState();

        InAirState();

        WallSlideState();

        //WallHangState();

    }

    private void SlideState()
    {
        if (isRunning && Input.GetKey(KeyCode.S)) {
            if (runDuration >= playerMovement.requiredRunDuration) {
                isSliding = true;
                isRunning = false;
                animator.SetBool("isSliding", true); //Animation for Sliding
            }
        } else if (playerMovement.timer_slideDuration >= playerMovement.slideDuration || isJumping) {
            runDuration = 0f;
            isSliding = false;
            animator.SetBool("isSliding", false);
        }
    }

    private void IdleState()
    {
        if (onGround && Input.GetAxis("Horizontal") == 0) {
            isIdle = true;
            animator.SetBool("isIdle", true); //Animation for Idle
        } else {
            isIdle = false;
            animator.SetBool("isIdle", false);
        }
    }

    private void InAirState()
    {
        if (!onGround && !isTouchingWall) {
            inAir = true;
        } else {
            inAir = false;
        }
    }

    private void JumpState()
    {
        if (onGround && Input.GetKey(KeyCode.Space) && playerMovement.timer_jumpDuration < playerMovement.jumpDuration) {
            isJumping = true;
        } else if (onGround)  {
            isJumping = false;
        }
    }

    private void RunState()
    {
        if (onGround && !isSliding && !isJumping) {

            if (Input.GetAxis("Horizontal") < -playerMovement.joystick_Threshold || Input.GetAxis("Horizontal") > playerMovement.joystick_Threshold) {
                isRunning = true;
                animator.SetBool("isRunning", true); //Animation for Running
                runDuration += Time.deltaTime;
            } else {
                isRunning = false;
                animator.SetBool("isRunning", false);
                
            }
        } else if(isJumping) {
            isRunning = false;
        } else if (!onGround) {
            isRunning = false;
        }
    }

    private void WallSlideState()
    {
        if (isTouchingWall && !onGround && playerMovement.timer_jumpDuration > playerMovement.jumpDuration /*<- not sure what that does*/)
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void CheckMovementDirection()
    {
        if (isFacingRight && playerMovement.movementInputDirection < 0)
        {
            Flip();
        }
        else if (!isFacingRight && playerMovement.movementInputDirection > 0)
        {
            Flip();
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            onGround = true;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground") {
            onGround = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground") {
            onGround = false;
        }
    }
}
