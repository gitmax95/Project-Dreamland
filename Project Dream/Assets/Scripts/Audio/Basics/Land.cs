using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Land : MonoBehaviour
{
    PlayerState playerState;

    [FMODUnity.EventRef]
    public string selectedSound;
    //FMOD.Studio.EventInstance soundEvent;

    bool inAir;



    // Start is called before the first frame update
    void Start()
    {

        playerState = GameObject.Find("PlayerChar").GetComponent<PlayerState>();

    }

    // Update is called once per frame
    void Update()
    {
        //FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundEvent, GetComponent<Transform>(), GetComponent<Rigidbody>());

        if (!playerState.isTouchingGround)
        {
            inAir = true;
        }

        if (inAir)
        {
            if (playerState.isTouchingGround)
            {
                
                    inAir = false;
                FMODUnity.RuntimeManager.PlayOneShot(selectedSound, GetComponent<Transform>().position);
                //soundEvent = FMODUnity.RuntimeManager.CreateInstance(selectedSound);
                //    soundEvent.start();
                
            }
        }
    }

    


}