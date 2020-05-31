using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    PlayerState playerState;
    PlayerHealthSystem playerHealth;
    LucidState lucidState;

    GameObject lucidIcon;

    [SerializeField]
    [FMODUnity.EventRef] string JumpingEvent = "event:/PlayerMechanics/Jumping";

    [SerializeField]
    [FMODUnity.EventRef] string[] LandingEvent = { "event:/PlayerMechanics/Landing/LandingWood",
                                                   "event:/PlayerMechanics/Landing/LandingStone",
                                                   "event:/PlayerMechanics/Landing/LandingCarpet",
                                                   "event:/PlayerMechanics/Landing/LandingMetal",
                                                   "event:/PlayerMechanics/Landing/LandingSand",
                                                   "event:/PlayerMechanics/Landing/LandingWater"};
    [FMODUnity.EventRef]
    public string Walking = "event:/PlayerMechanics/Walking";

    [FMODUnity.EventRef]
    public string Damage = "event:/PlayerMechanics/Damage";

    [FMODUnity.EventRef]
    public string Lucid = "event:/PlayerMechanics/Lucid";

    FMOD.Studio.EventInstance soundEvent;

    //[FMODUnity.EventRef]
    //public string Sliding = "event:/SFX/Sliding";

    //Variables to keep track of what sounds are allowed to be played atm

    bool inAir;
    bool onGround;

    //Parameter values: Wood - 0, Stone - 1, Carpet - 2, Metal - 3, Sand - 4, Water - 5, inAir/Idle - 6.
    int parameterValue;
    int memoryValue;

    // Start is called before the first frame update
    void Start()
    {
        
        playerState = this.gameObject.GetComponent<PlayerState>();
        playerHealth = this.gameObject.GetComponent<PlayerHealthSystem>();

        soundEvent = FMODUnity.RuntimeManager.CreateInstance(Walking);
        soundEvent.start();

        

    }

    // Update is called once per frame
    void Update()
    {
        lucidIcon = GameObject.Find("LucidState");

        if (lucidIcon != null)
        {
            lucidState = GameObject.Find("LucidIcon").GetComponent<LucidState>();
        }
                
        
        LucidSFX();
        JumpSFX();
        WalkingSFX();
        LandingSFX();
        //SlidingSFX();

        soundEvent.setParameterByName("PlayerMSFX", parameterValue);


    }

    void LucidSFX()
    {
        if (lucidState != null)
        {
            if (lucidState.isLucid)
            {
                print("lucid exists and should play");
                FMODUnity.RuntimeManager.PlayOneShot(Lucid, GetComponent<Transform>().position);
            }
        }
        
    }

    //void DamageSFX()
    //{
    //    if (playerHealth.playerHealth != 10)
    //    {
    //        if (initialHealth > playerHealth.playerHealth)
    //        {
    //            FMODUnity.RuntimeManager.PlayOneShot(Damage, GetComponent<Transform>().position);
    //            //damageSoundEvent.setParameterByName("DamageIntensity", playerHealth.playerHealth);
    //        }
    //        else if(initialHealth == playerHealth.playerHealth)
    //        {
    //            //damageSoundEvent.setParameterByName("DamageIntensity", 10);
    //        }
    //        else if(initialHealth < playerHealth.playerHealth)
    //        {
    //            initialHealth = playerHealth.playerHealth;
    //        }

    //        initialHealth = playerHealth.playerHealth;
    //    }
    //    else
    //    {
    //        //damageSoundEvent.setParameterByName("DamageIntensity", 10); /silence
    //    }
    //}

    void JumpSFX()
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
            FMODUnity.RuntimeManager.PlayOneShot(JumpingEvent, GetComponent<Transform>().position);
            onGround = false;
        }




    }

    void LandingSFX()
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
                    case 5:
                        FMODUnity.RuntimeManager.PlayOneShot(LandingEvent[5], GetComponent<Transform>().position);
                        inAir = false;
                        break;
                }

            }
        }


    }

    void WalkingSFX()
    {
        if (playerState.isRunning)
        {
            parameterValue = memoryValue;

        }
        else if(!playerState.isRunning)
        {

            parameterValue = 6; //Idle sound

        }
        
    }

    //void SlidingSFX()
    //{
    //   
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
                else if (surface.getType() == "Water")
                {
                    parameterValue = 5;
                    memoryValue = 5;
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
