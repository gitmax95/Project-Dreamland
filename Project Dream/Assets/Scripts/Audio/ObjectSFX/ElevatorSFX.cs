using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorSFX : MonoBehaviour
{
    //bool bellSound = false;

    //[FMODUnity.EventRef]
    //public string Elevator = "event:/SFX/Elevator";
    //[FMODUnity.EventRef]
    //public string Bell = "event:/SFX/Bell";

    //FMOD.Studio.EventInstance soundEvent;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    //AttachInstanceToGameObject(FMOD.Studio.EventInstance instance, Transform transform, Rigidbody rigidBody)

    //    soundEvent = FMODUnity.RuntimeManager.CreateInstance(Elevator);
    //    soundEvent.start();
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if(isMoving)
    //    {
    //        bellSound = true;
    //    }

    //    if(destination == transform.position)
    //    {
    //        if(bellSound)
    //        {
    //            FMODUnity.RuntimeManager.PlayOneShot(Bell, GetComponent<Transform>().position);
    //            bellSound = false;
    //        }
    //    }

    //    if(isMoving)
    //    {
    //        soundEvent.setParameterByName("ElevatorP", 1);
    //    }
    //    else
    //    {
    //        soundEvent.setParameterByName("ElevatorP", 0);
    //    }
    //}
}
