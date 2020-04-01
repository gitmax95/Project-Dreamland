using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement_Player : MonoBehaviour
{
    PlayerState playerState;

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

    int direction_Horizontal;
    public float movementInputDirection;

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
    public float wallSlideSpeed;
    bool newFeetLocation;

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
        playerState = GameObject.Find("Player").GetComponent<PlayerState>();
        rigidBodyPlayer = GameObject.Find("Player").GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        CheckInput();

        if (playerState.onGround) {
            rigidBodyPlayer.velocity = Vector2.zero;
        }

        if (Input.GetAxis("Horizontal") < 0) {
            direction_Horizontal = -1;
        } else if (Input.GetAxis("Horizontal") > 0) {
            direction_Horizontal = 1;
        }

        if (!playerState.isSliding) {
        timer_timeBetweenSlide += Time.deltaTime; //Track timeBetweenSlides
        }

      
    }

    private void FixedUpdate()
    {
        CheckSuroundings();

        if (playerState.isIdle) { //Player is Idle
            currentStrafeSpeed = strafeSpeedIdle;
            rigidBodyPlayer.velocity = new Vector2(0, 0); //TODO: IMPROVE THIS WITH A INPUT CHECK FOR JOYSTICK TOUCH. THIS WILL INSTANTLY NOTICE A LACK OF INPUT.
        }

        if (playerState.isRunning) { //Player is Running
            //RunWithForce();
            RunByTranslate();
            currentStrafeSpeed = strafeSpeedRun;
        }


        if (playerState.isSliding) {  //Player is Sliding          
            Slide();
            
        } else {
            timer_slideDuration = 0f;
            
        }

        if(!playerState.isSliding && newFeetLocation) { //Set new position when Player stands up after a slide
            timer_timeBetweenSlide = 0f;
            newFeetLocation = false;
            Vector3 newPosition = new Vector3(transform.position.x + (0.38f * direction_Horizontal), transform.position.y, transform.position.z);
            //transform.position = new Vector3(transform.position.x + (0.38f * direction_Horizontal), transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, newPosition, 10 * Time.deltaTime);
            
        }
         
        if(playerState.isJumping) { //Player is Jumping
            Jump();
            
        } else if (playerState.onGround) { //Reset Jump Timer after the completion of a jump.
            timer_jumpDuration = 0f;
        }

        if (playerState.inAir) {
            AirStrafe();
        }

        if (playerState.isWallSliding)
        {
            WallSlide();
        }

    }

    private void CheckInput()
    {
        movementInputDirection = Input.GetAxisRaw("Horizontal");
    }

    private void Slide()
    {
        timer_slideDuration += Time.deltaTime;

        newFeetLocation = true;

        if(timer_timeBetweenSlide >= timeBetweenSlide) {
            currentStrafeSpeed = strafeSpeedSlide;
            rigidBodyPlayer.velocity = new Vector2(slideSpeed * direction_Horizontal * Time.deltaTime, rigidBodyPlayer.velocity.y);
        } else {
            currentStrafeSpeed = strafeSpeedSlide * speedDecayMultiplier;
            rigidBodyPlayer.velocity = new Vector2((slideSpeed * speedDecayMultiplier) * direction_Horizontal * Time.deltaTime, rigidBodyPlayer.velocity.y);
        }
    }

    private void AirStrafe()
    {
        if (Input.GetAxis("Horizontal") > joystick_Threshold) {

            rigidBodyPlayer.velocity = new Vector2(currentStrafeSpeed * Time.deltaTime, rigidBodyPlayer.velocity.y);

        } else if (Input.GetAxis("Horizontal") < -joystick_Threshold) {

            rigidBodyPlayer.velocity = new Vector2(-currentStrafeSpeed * Time.deltaTime, rigidBodyPlayer.velocity.y);

        } else {
            rigidBodyPlayer.velocity = new Vector2(0, rigidBodyPlayer.velocity.y); //Remove this to keep momentum even without Strafe Input.
        }
    }

    private void Jump()
    {
        print("Jumping");
        timer_jumpDuration += Time.deltaTime;

        if(timer_jumpDuration < jumpDuration && Input.GetKey(KeyCode.Space))
        {
        rigidBodyPlayer.velocity = new Vector2(rigidBodyPlayer.velocity.x, jumpVelocity * Time.deltaTime);
        }

        if (rigidBodyPlayer.velocity.y < 0)
        { //Player is Falling
            rigidBodyPlayer.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rigidBodyPlayer.velocity.y > 0 && timer_jumpDuration < jumpDuration / 2 && !Input.GetKey(KeyCode.Space))
        {
            rigidBodyPlayer.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    private void RunWithForce()
    {
        if (Input.GetAxis("Horizontal") > joystick_Threshold) {

            if (rigidBodyPlayer.velocity.x < runMaxVelocity.x) {

                rigidBodyPlayer.AddForce(transform.right * direction_Horizontal * (runAcceleration * 10) * Time.deltaTime, 0);
               
            }
        } else if (Input.GetAxis("Horizontal") < -joystick_Threshold) {

            if (rigidBodyPlayer.velocity.x > -runMaxVelocity.x) {

                rigidBodyPlayer.AddForce(transform.right * direction_Horizontal * (runAcceleration * 10) * Time.deltaTime, 0);
 
            }
        } else {
          rigidBodyPlayer.velocity = new Vector2(0, rigidBodyPlayer.velocity.y);
  
        }
    }

    private void RunByTranslate()
    {
        if(Input.GetAxis("Horizontal") < -joystick_Threshold || Input.GetAxis("Horizontal") > joystick_Threshold) {
            transform.Translate(transform.right * direction_Horizontal * runSpeed * Time.deltaTime);
        }
    }

    private void WallSlide()
    {
        if (rigidBodyPlayer.velocity.y < -wallSlideSpeed)
        {
            rigidBodyPlayer.velocity = new Vector2(rigidBodyPlayer.velocity.x, -wallSlideSpeed);
        }
    }

    private void CheckSuroundings()
    {

        //if (movementInputDirection > 0)
        //{
        //    playerState.isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);
        //}
        //else if (movementInputDirection < 0)
        //{
        //    playerState.isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);
        //}
        playerState.isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);
    }

    private void OnDrawGizmos()
    {
        //if (playerState.isFacingRight) //This gives an error (no clue why, game runs fine). However, these are just to see the wall detection and ccan 
        //{
        //    Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
        //}
        //else if (!playerState.isFacingRight)
        //{
        //    Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x-+ wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
        //}
    }
}
