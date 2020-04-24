using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    PlayerState playerState;

    [FMODUnity.EventRef]
    public string Jumping = "event:/SFX/Jumping";

    [FMODUnity.EventRef]
    public string Landing = "event:/SFX/Landing";

    [FMODUnity.EventRef]
    public string Walking = "event:/SFX/Walking";

    [FMODUnity.EventRef]
    public string Sliding = "event:/SFX/Sliding";

    FMOD.Studio.EventInstance soundEvent;

    //Variables to keep track of what sounds are allowed to be played atm

    bool inAir;
    bool onGround;


    // Start is called before the first frame update
    void Start()
    {

        playerState = GameObject.Find("PlayerChar").GetComponent<PlayerState>();


    }

    // Update is called once per frame
    void Update()
    {
        PlayJumpSFX();
        PlayLandingSFX();
        PlayWalkingSFX();
        PlaySlidingSFX();

        
    }

    void PlayJumpSFX()
    {

        if (!playerState.isJumping && playerState.isTouchingGround)
        {
            onGround = true;

        }
        else if(!playerState.isTouchingGround)
        {
            onGround = false;
        }
           

        if (playerState.isJumping && onGround)
        {

            print("jump sound");
            FMODUnity.RuntimeManager.PlayOneShot(Jumping, GetComponent<Transform>().position);
            onGround = false;
            
         
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

                inAir = false;
                FMODUnity.RuntimeManager.PlayOneShot(Landing, GetComponent<Transform>().position);
                
            }
        }


    }

    void PlayWalkingSFX()
    {

        
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundEvent, GetComponent<Transform>(), GetComponent<Rigidbody>());



        if (playerState.isRunning)
        {
            
            soundEvent = FMODUnity.RuntimeManager.CreateInstance(Walking);
            soundEvent.start();
            soundEvent.release();

        }

    }

    void PlaySlidingSFX()
    {
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundEvent, GetComponent<Transform>(), GetComponent<Rigidbody>());

        if (playerState.isSliding || playerState.isWallSliding)
        {
            soundEvent = FMODUnity.RuntimeManager.CreateInstance(Landing);
            soundEvent.start();
            soundEvent.release();

        }
    }


}
