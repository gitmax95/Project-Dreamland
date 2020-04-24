using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    PlayerState player;
 
    [FMODUnity.EventRef]
    public string selectedSound;
    //FMOD.Studio.EventInstance soundEvent;
    
    bool playsound;

    

    // Start is called before the first frame update
    void Start()
    {
      
        player = GameObject.Find("PlayerChar").GetComponent<PlayerState>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundEvent, GetComponent<Transform>(), GetComponent<Rigidbody>());


        if (player.isJumping && playsound)
        {

            playsound = false;
            JumpSound();         
             
        }

        if (!player.isJumping && player.isTouchingGround)
        {
            playsound = true;
            
        }

    }

    void JumpSound()
    {

        FMODUnity.RuntimeManager.PlayOneShot(selectedSound, GetComponent<Transform>().position); //PlayOneShotAttached

        //soundEvent = FMODUnity.RuntimeManager.CreateInstance(selectedSound);
        //    soundEvent.start();
                   
    }


}
