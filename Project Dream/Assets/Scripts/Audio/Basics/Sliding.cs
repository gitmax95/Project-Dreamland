using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sliding : MonoBehaviour
{

    PlayerState player;

    [FMODUnity.EventRef]
    public string selectedSound;
    FMOD.Studio.EventInstance soundEvent;

    bool playsound;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlayerChar").GetComponent<PlayerState>();

    }

    // Update is called once per frame
    void Update()
    {
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundEvent, GetComponent<Transform>(), GetComponent<Rigidbody>());

        if(player.isSliding || player.isWallSliding)
        {
            SlideSound();
          
        }
                

    }

    void SlideSound()
    {
        soundEvent = FMODUnity.RuntimeManager.CreateInstance(selectedSound);
        soundEvent.start();
        soundEvent.release();
    }
}
