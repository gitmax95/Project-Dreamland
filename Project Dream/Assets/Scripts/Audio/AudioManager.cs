using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    PlayerState playerState;

    [SerializeField]
    [FMODUnity.EventRef] string[] JumpingEvent = { "event:/Jumping/JumpingWood", "event:/Jumping/JumpingStone", "event:/Jumping/JumpingCarpet", "event:/Jumping/JumpingMetal", "event:/Jumping/JumpingSand"};

    [SerializeField]
    [FMODUnity.EventRef] string[] LandingEvent = { "event:/Landing/LandingWood", "event:/Landing/LandingStone", "event:/Landing/LandingCarpet", "event:/Landing/LandingMetal", "event:/Landing/LandingSand"};

    //Walking Event
    [FMODUnity.EventRef]
    public string Walking = "event:/SFX/walk";

    FMOD.Studio.EventInstance soundEvent;

    //[FMODUnity.EventRef]
    //public string Sliding = "event:/SFX/Sliding";
    
    //Variables to keep track of what sounds are allowed to be played atm

    bool inAir;
    bool onGround;

    //Parameter values: Wood - 0, Stone - 1, Carpet - 2, Metal - 3, inAir/Idle - 4.
    int parameterValue;
    int memoryValue; 


    // Start is called before the first frame update
    void Start()
    {

        //JumpingEvent[0] = "event:/Jumping/JumpingWood";
        //JumpingEvent[1] = "event:/Jumping/JumpingStone";
        //JumpingEvent[2] = "event:/Jumping/JumpingCarpet";
        //JumpingEvent[3] = "event:/Jumping/JumpingMetal";
        //JumpingEvent[4] = "event:/Jumping/JumpingSand";

        //LandingEvent[0] = "event:/Landing/LandingWood";
        //LandingEvent[1] = "event:/Landing/LandingStone";
        //LandingEvent[2] = "event:/Landing/LandingCarpet";
        //LandingEvent[3] = "event:/Landing/LandingMetal";
        //LandingEvent[4] = "event:/Landing/LandingSand";

        playerState = GameObject.Find("PlayerChar").GetComponent<PlayerState>();


        soundEvent = FMODUnity.RuntimeManager.CreateInstance(Walking);
        soundEvent.start();

    }

    // Update is called once per frame
    void Update()
    {


        PlayJumpSFX();
        PlayWalkingSFX();
        PlayLandingSFX();
        //PlaySlidingSFX();

        soundEvent.setParameterByName("PlayerMSFX", parameterValue);



        //if (Input.GetAxis("Horizontal") >= 0.01f || Input.GetAxis("Horizontal") <= -0.01f)
        //{
        //    
        //}
        //else if(Input.GetAxis("Horizontal") == 0)
        //{

        //}

    }

    void PlayJumpSFX()
    {

        if (!playerState.isJumping && playerState.isTouchingGround)
        {
            onGround = true;
          
        }
        else if (!playerState.isTouchingGround)
        {
            onGround = false;
            
        }


        if (playerState.isJumping && onGround)
        {

            switch (memoryValue)
            {
                case 0:
                    FMODUnity.RuntimeManager.PlayOneShot(JumpingEvent[0], GetComponent<Transform>().position);
                    onGround = false;
                    break;

                case 1:
                    FMODUnity.RuntimeManager.PlayOneShot(JumpingEvent[1], GetComponent<Transform>().position);
                    onGround = false;
                    break;

                case 2:
                    FMODUnity.RuntimeManager.PlayOneShot(JumpingEvent[2], GetComponent<Transform>().position);
                    onGround = false;
                    break;

                case 3:
                    FMODUnity.RuntimeManager.PlayOneShot(JumpingEvent[3], GetComponent<Transform>().position);
                    onGround = false;
                    break;
                case 4:
                    FMODUnity.RuntimeManager.PlayOneShot(JumpingEvent[4], GetComponent<Transform>().position);
                    onGround = false;
                    break;

            }           
        }




    }

    void PlayLandingSFX()
    {

        if (!playerState.isTouchingGround)
        {
            inAir = true;
           
        }

        if (inAir)
        {
            if (playerState.isTouchingGround)
            {
               

                switch (memoryValue)
                {
                    case 0:
                        FMODUnity.RuntimeManager.PlayOneShot(LandingEvent[0], GetComponent<Transform>().position);
                        inAir = false;
                        break;

                    case 1:
                        FMODUnity.RuntimeManager.PlayOneShot(LandingEvent[1], GetComponent<Transform>().position);
                        inAir = false;
                        break;

                    case 2:
                        FMODUnity.RuntimeManager.PlayOneShot(LandingEvent[2], GetComponent<Transform>().position);
                        inAir = false;
                        break;

                    case 3:
                        FMODUnity.RuntimeManager.PlayOneShot(LandingEvent[3], GetComponent<Transform>().position);
                        inAir = false;
                        break;
                    case 4:
                        FMODUnity.RuntimeManager.PlayOneShot(LandingEvent[4], GetComponent<Transform>().position);
                        inAir = false;
                        break;
                }

            }
        }


    }

    void PlayWalkingSFX()
    {
        if (playerState.isRunning)
        {
            parameterValue = memoryValue;

        }
        else if(!playerState.isRunning)
        {

            parameterValue = 5; //Idle sound

        }
        
    }

    //void PlaySlidingSFX()
    //{
    //    FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundEvent, GetComponent<Transform>(), GetComponent<Rigidbody>());

    //    if (playerState.isSliding || playerState.isWallSliding)
    //    {
    //        soundEvent = FMODUnity.RuntimeManager.CreateInstance(Landing);
    //        soundEvent.start();
            

    //    }
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {

        GameObject other = collision.gameObject;

        if (other.gameObject.tag == "Ground")
        {
            SurfaceType surface;
            surface = other.GetComponent<SurfaceType>();

            if (surface != null)
            {
                if (surface.getType() == "Wood")
                {
                    parameterValue = 0;
                    memoryValue = 0;
                }
                else if (surface.getType() == "Stone")
                {
                    parameterValue = 1;
                    memoryValue = 1;
                }               
                else if (surface.getType() == "Carpet")
                {

                    parameterValue = 2;
                    memoryValue = 2;
                }
                else if (surface.getType() == "Metal")
                {
                    parameterValue = 3;
                    memoryValue = 3;
                }
                else if (surface.getType() == "Sand")
                {
                    parameterValue = 4;
                    memoryValue = 4;
                }


            }
            else
            {
                parameterValue = 1;
                memoryValue = 1;
            }

        }

    }
}
