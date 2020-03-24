using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    BasicMovement_Player playerMovement;

    PolygonCollider2D playerCollider;

    public Animator animator;

    [Header("Player States")]
    public bool isIdle;
    public bool onGround;
    public bool isRunning;
    public bool isSliding;
    public bool isJumping;  
    public bool inAir;

    float runDuration;

    private void Start()
    {
        playerMovement = GameObject.Find("Player").GetComponent<BasicMovement_Player>();
    }
    void Update()
    {
        IdleState();

        RunState();

        if(isRunning && Input.GetKey(KeyCode.S)) {
            if(runDuration >= playerMovement.requiredRunDuration) {
            isSliding = true;
            isRunning = false;
            }
        } else if(playerMovement.timer_slideDuration >= playerMovement.slideDuration || isJumping) {
            runDuration = 0f;
            isSliding = false;
        }

        JumpState();

        InAirState();

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
        if (!onGround) {
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground") {
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
