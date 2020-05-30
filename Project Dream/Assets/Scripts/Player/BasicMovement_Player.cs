using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement_Player : MonoBehaviour
{
    PlayerState playerState;
    ControllerStates controllerStates;

    public Rigidbody2D rigidBodyPlayer;
    public Transform wallCheck;

    public LayerMask whatIsGround;

    [Header("Player Running")]
    [Tooltip("Player accelerates with Run Acceleration each frame.")]
    public float runAcceleration = 50;
    [Tooltip("Player stops acceleration at Max Velocity.")]
    public Vector2 runMaxVelocity;
    [Tooltip("Player Run if not using Force")]
    public float runSpeed = 10f;

    public float wallCheckDistance;
    public float movementInputDirection;
    int directionHorizontal;
    int slideDirection;

    [Tooltip("Changes where the Joystick will notice the Player input.")]
    public float joystick_Threshold;

    [Header("Player Sliding")]
    public float requiredRunDuration;
    public float slideSpeed;
    public float slideDuration;
    public float timer_slideDuration;
    public float timeBetweenSlide = 0.5f;
    public float timer_timeBetweenSlide = 0.5f;
    public float speedDecayMultiplier = 0.5f;
    public float timerWallJump = 0.0f;
    public float wallSlideSpeed;
    public float movementForceInAir;
    public float wallHopForce;
    public float wallJumpForce;

    bool newFeetLocation;
    bool canStrafe = true;
    bool directionLock = false;
    bool resetY;

    Vector2 deathPos;
    Vector2 targetPos;

    [Header("Player Jumping")]
    [Tooltip("Speed of the upwards motion of the Jump")]
    [Range(1, 200)]
    public float jumpVelocity;
    [Tooltip("Time which the Velocity is applied")]
    public float jumpDuration = 0.1f;
    [Tooltip("Timer for jumpDuration")]
    public float timer_jumpDuration;
    [Tooltip("Additional Gravity applied during a High Jump")]
    public float fallMultiplier = 2.5f;
    [Tooltip("Additional Gravity applied during a Short Jump")]
    public float lowJumpMultiplier = 2.0f;

    [Header("Player In Air")]   
    [Tooltip("Applied Air Strafe speed when Jumping from Idle")]
    public float strafeSpeedIdle;
    [Tooltip("Applied Air Strafe speed when Jumping while Running")]
    public float strafeSpeedRun;
    [Tooltip("Applied Air Strafe speed when Jumping while Sliding")]
    public float strafeSpeedSlide;
    float currentStrafeSpeed; //Assigned when Idle, Running, Sliding


    void Start()
    {
        playerState = GameObject.Find("PlayerChar").GetComponent<PlayerState>();
        controllerStates = GameObject.Find("InputManager").GetComponent<ControllerStates>();

        rigidBodyPlayer = GameObject.Find("PlayerChar").GetComponent<Rigidbody2D>();

        resetY = false;
    }

    private void Update()
    {
        CheckInput();
        CheckMovementDirection();
        //CheckStrafe();
        //CheckWallJumpState();
        LockDirection();

        if (playerState.isTouchingGround)
        {
            rigidBodyPlayer.velocity = Vector2.zero;
        }

        if (controllerStates.input_Horizontal < 0 && !playerState.isSliding)
        {
            directionHorizontal = -1;
        }
        else if (controllerStates.input_Horizontal > 0 && !playerState.isSliding)
        {
            directionHorizontal = 1;
        }

        if (!playerState.isSliding) {
        timer_timeBetweenSlide += Time.deltaTime; //Track timeBetweenSlides
        }
        if (playerState.isDying)
        {
            if (!resetY)
            {
                //rigidBodyPlayer.velocity = new Vector2(rigidBodyPlayer.velocity.x, 0.0f);
                resetY = true;
                deathPos.y = transform.position.y;
                targetPos.y = deathPos.y + 2.0f;
            }
            rigidBodyPlayer.velocity = new Vector2(0.0f, Mathf.MoveTowards(deathPos.y, targetPos.y, 0.5f));
        }     
    }

    private void FixedUpdate()
    {
        if (!playerState.isDying && !playerState.isDead)
        {
            //CheckSuroundings();

            if (playerState.isIdle)
            { //Player is Idle
                currentStrafeSpeed = strafeSpeedIdle;
                rigidBodyPlayer.velocity = new Vector2(0, 0); //TODO: IMPROVE THIS WITH A INPUT CHECK FOR JOYSTICK TOUCH. THIS WILL INSTANTLY NOTICE A LACK OF INPUT.
            }

            if (playerState.isRunning)
            { //Player is Running
              //RunWithForce();
                RunByTranslate();
                currentStrafeSpeed = strafeSpeedRun;
            }


            if (playerState.isSliding)
            {  //Player is Sliding          
                Slide();

            }
            else
            {
                timer_slideDuration = 0.0f;

            }

            if (!playerState.isSliding && newFeetLocation)
            { //Set new position when Player stands up after a slide
                timer_timeBetweenSlide = 0.0f;
                newFeetLocation = false;
                Vector3 newPosition = new Vector3(transform.position.x + (0.38f * slideDirection), transform.position.y, transform.position.z);
                //transform.position = new Vector3(transform.position.x + (0.38f * directionHorizontal), transform.position.y, transform.position.z);
                transform.position = Vector3.Lerp(transform.position, newPosition, 10 * Time.deltaTime);

            }

            if (playerState.isJumping/* || playerState.isWallJumping*/)
            {
                //Player is Jumping
                if (playerState.isTouchingGround)
                {
                    Jump();
                }
                //Player is WallJumping
                //else if (playerState.isWallSliding)
                //{
                //    WallJump();
                //}


                //if (!playerState.isTouchingGround && playerState.isWallSliding && Input.GetKey(KeyCode.Space))
                //{
                //    AddForce();
                //}

            }
            else if (playerState.isTouchingGround)
            { //Reset Jump Timer after the completion of a jump.
                timer_jumpDuration = 0.0f;
            }

            if (!playerState.isGrounded)
            {
                AirStrafe();
            }

            //if (playerState.isWallSliding)
            //{
            //    WallSlide();
            //}

            //if (!playerState.isTouchingGround && playerState.isWallSliding && movementInputDirection != 0)
            //{
            //    AddForce();
            //}
        }
    }

    private void CheckInput()
    {
        movementInputDirection = controllerStates.input_Horizontal; //Replace as well with controllerStates.inputHorizontal? Input.GetAxisRaw("Horizontal")
    }

    private void Slide()
    {
        timer_slideDuration += Time.deltaTime;

        newFeetLocation = true;

        if(timer_timeBetweenSlide >= timeBetweenSlide)
        {
            currentStrafeSpeed = strafeSpeedSlide;
            rigidBodyPlayer.velocity = new Vector2(slideSpeed * slideDirection * Time.deltaTime, rigidBodyPlayer.velocity.y);
        }
        else
        {
            currentStrafeSpeed = strafeSpeedSlide * speedDecayMultiplier;
            rigidBodyPlayer.velocity = new Vector2((slideSpeed * speedDecayMultiplier) * slideDirection * Time.deltaTime, rigidBodyPlayer.velocity.y);
        }
    }
    
    private void AirStrafe()
    {
        if (canStrafe)
        {
            if (controllerStates.input_Horizontal > joystick_Threshold)
            {
                rigidBodyPlayer.velocity = new Vector2(currentStrafeSpeed * Time.deltaTime, rigidBodyPlayer.velocity.y);
            }

            else if (controllerStates.input_Horizontal < -joystick_Threshold)
            {
                rigidBodyPlayer.velocity = new Vector2(-currentStrafeSpeed * Time.deltaTime, rigidBodyPlayer.velocity.y);
            }

           else if (controllerStates.input_Horizontal == 0)  //All aditions are temp while I am working on Wall Jump, the reason for this so the player deosnt "run" off the wall when sliding
           {
                rigidBodyPlayer.velocity = new Vector2(0, rigidBodyPlayer.velocity.y); //Remove this to keep momentum even without Strafe Input.
           }
        }
    }

    private void Jump()
    {
        print("Jumping");
        timer_jumpDuration += Time.deltaTime;

        if(timer_jumpDuration < jumpDuration && playerState.isJumping)
        {
            rigidBodyPlayer.velocity = new Vector2(rigidBodyPlayer.velocity.x, jumpVelocity * Time.deltaTime);
        }

        /* if (rigidBodyPlayer.velocity.y < 0) //Player is Falling - THIS SECTION DOES NOTHING ANYMORE. REWORK?
        { 
            rigidBodyPlayer.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rigidBodyPlayer.velocity.y > 0 && timer_jumpDuration < jumpDuration / 2 && !Input.GetKeyUp(KeyCode.Space))
        {
            rigidBodyPlayer.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        } */
    }

    //private void WallJump()
    //{
    //    print("Wall Jumping");

    //    if (playerState.isWallSliding && playerState.isTouchingWall && Input.GetKey(KeyCode.Space))
    //    {
    //        if (playerState.touchingRightWall)
    //        {
    //            rigidBodyPlayer.velocity = new Vector2(-jumpVelocity * 0.5f * Time.deltaTime, jumpVelocity*0.75f * Time.deltaTime);
    //            //Flip();
    //        }
    //        else if (playerState.touchingLeftWall)
    //        {
    //            rigidBodyPlayer.velocity = new Vector2(jumpVelocity * 0.5f * Time.deltaTime, jumpVelocity*0.75f * Time.deltaTime);
    //            //Flip();
    //        }
    //        Flip();
    //    }
    //}
    
   /* private void RunWithForce()
    {
        if (Input.GetAxis("Horizontal") > joystick_Threshold) {

            if (rigidBodyPlayer.velocity.x < runMaxVelocity.x) {

                rigidBodyPlayer.AddForce(transform.right * directionHorizontal * (runAcceleration * 10) * Time.deltaTime, 0);
               
            }
        } else if (Input.GetAxis("Horizontal") < -joystick_Threshold) {

            if (rigidBodyPlayer.velocity.x > -runMaxVelocity.x) {

                rigidBodyPlayer.AddForce(transform.right * directionHorizontal * (runAcceleration * 10) * Time.deltaTime, 0);
 
            }
        } else {
          rigidBodyPlayer.velocity = new Vector2(0, rigidBodyPlayer.velocity.y);
  
        }
    }*/

    private void RunByTranslate()
    {
        if (!playerState.isSliding)
        {
            if (controllerStates.input_Horizontal < -joystick_Threshold || controllerStates.input_Horizontal > joystick_Threshold && playerState.isGrounded)  //Temporary, will get a better fix asap 
            {
                transform.Translate(transform.right * directionHorizontal * runSpeed * Time.deltaTime);
            }
        }
    }

    //private void WallSlide()
    //{
    //    if (rigidBodyPlayer.velocity.y < -wallSlideSpeed)
    //    {
    //        rigidBodyPlayer.velocity = new Vector2(rigidBodyPlayer.velocity.x, -wallSlideSpeed);
    //        //if (playerState.isTouchingWall)
    //        //{
    //        //    timer_jumpDuration = 0.0f;
    //        //}
    //    }
    //}

    private void CheckMovementDirection()
    {
        if (playerState.isFacingRight && movementInputDirection < 0)
        {
            Flip();
        }
        else if (!playerState.isFacingRight && movementInputDirection > 0)
        {
            Flip();
        }
    }

    private void Flip()
    {
        if (!playerState.isDying && !playerState.isSliding)
        {
            directionHorizontal *= -1;
            playerState.isFacingRight = !playerState.isFacingRight;
            transform.Rotate(0.0f, 180.0f, 0.0f);
        }
    }

    //private void CheckStrafe()
    //{
    //    if (timerWallJump > 0 && timerWallJump < 0.5f)
    //    {
    //        canStrafe = false;
    //    }
    //    else if (timerWallJump == 0)
    //    {
    //        canStrafe = true;
    //    }
    //    else
    //    {
    //        canStrafe = true;
    //    }    
    //}

    //private void CheckWallJumpState()
    //{
    //    if (playerState.isWallJumping)
    //    {
    //        timerWallJump += Time.deltaTime;
    //    }
    //    else if (!playerState.isWallJumping)
    //    {
    //        timerWallJump = 0.0f;
    //    }
    //}
    //private void AddForce()
    //{
    //    Vector2 forceToAdd = new Vector2(movementForceInAir * movementInputDirection, 0);
    //    rigidBodyPlayer.AddForce(forceToAdd);

    //    if (Mathf.Abs(rigidBodyPlayer.velocity.x) > runSpeed)
    //    {
    //        rigidBodyPlayer.velocity = new Vector2(runSpeed * movementInputDirection, rigidBodyPlayer.velocity.y);
    //    }
    //}

    //private void CheckSuroundings()
    //{
    //    //if (movementInputDirection > 0)
    //    //{
    //    //    playerState.isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);
    //    //}
    //    //else if (movementInputDirection < 0)
    //    //{
    //    //    playerState.isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);
    //    //}
    //    playerState.isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);

    //    if (playerState.isFacingRight && playerState.isTouchingWall)
    //    {
    //        playerState.touchingRightWall = true;
    //    }
    //    else if (!playerState.isFacingRight && playerState.isTouchingWall)
    //    {
    //        playerState.touchingLeftWall = true;
    //    }
    //    else if (!playerState.isTouchingWall)
    //    {
    //        playerState.touchingLeftWall = false;
    //        playerState.touchingRightWall = false;
    //    }
    //}

    private void LockDirection()
    {
        //if (!playerState.isSliding)
        //{
        //    slideDirection = directionHorizontal;
        //}
        //else if (playerState.isSliding)
        //{
            if (playerState.isFacingRight)
            {
                slideDirection = 1;
            }
            else if (!playerState.isFacingRight)
            {
                slideDirection = -1;
            }
        //}
    }

    private void OnDrawGizmos()
    {
        //if (playerState.isFacingRight) //This gives an error (no clue why, game runs fine). However, these are just to see the wall detection and ccan 
        //{
        //    Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
        //}
        //else if (!playerState.isFacingRight)
        //{
        //    Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x - +wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
        //}
    }
}
