using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorSFX : MonoBehaviour
{
    bool bellSound = false;
    ElevatorMover elevator;

    [FMODUnity.EventRef]
    public string Elevator = "event:/SFX/Elevator";
    [FMODUnity.EventRef]
    public string Filler = "event:/SFX/FillerTone";

    //FMOD.Studio.EventInstance soundEvent;
    //Transform trans;
    //Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        //    FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundEvent, trans, rb);

        //    soundEvent = FMODUnity.RuntimeManager.CreateInstance(Elevator);
        //    soundEvent.start();

        elevator = this.gameObject.GetComponent<ElevatorMover>();
        //trans = this.gameObject.GetComponent<Transform>();
        //rb = this.gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        if(elevator.elevatorMoving)
        {
            FMODUnity.RuntimeManager.PlayOneShot(Elevator, GetComponent<Transform>().position);
        //soundEvent.setParameterByName("ElevatorP", 0f);
        }
       
    }
}
