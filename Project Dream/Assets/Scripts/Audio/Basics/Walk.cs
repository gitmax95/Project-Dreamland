using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : MonoBehaviour
{
    PlayerState player;

    [FMODUnity.EventRef]
    public string selectedSound;
    FMOD.Studio.EventInstance soundEvent;



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlayerChar").GetComponent<PlayerState>();

        
       
    }

    // Update is called once per frame
    void Update()
    {

        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundEvent, GetComponent<Transform>(), GetComponent<Rigidbody>());

       

        if(player.isRunning)
        {
            WalkSound();
            
        }
    }

    void WalkSound()
    {
        soundEvent = FMODUnity.RuntimeManager.CreateInstance(selectedSound);
        soundEvent.start();
        soundEvent.release();
    }

    
}
