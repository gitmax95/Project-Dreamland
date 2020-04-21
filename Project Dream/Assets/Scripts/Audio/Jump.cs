using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    PlayerState playerState;

    [FMODUnity.EventRef]
    public string selectedSound;
    FMOD.Studio.EventInstance soundEvent;

    

    // Start is called before the first frame update
    void Start()
    {
      
        playerState = GameObject.Find("Player").GetComponent<PlayerState>();

    }

    // Update is called once per frame
    void Update()
    {
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundEvent, GetComponent<Transform>(), GetComponent<Rigidbody>());

        if (playerState.isJumping == true)
        {
            soundEvent = FMODUnity.RuntimeManager.CreateInstance(selectedSound);
            soundEvent.start();
        

        }
    }


}
