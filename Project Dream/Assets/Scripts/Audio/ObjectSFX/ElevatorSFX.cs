using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorSFX : MonoBehaviour
{
    bool bellSound = false;
    ElevatorMover elevator;

    [FMODUnity.EventRef]
    public string Elevator = "event:/SFX/Elevator";

    FMOD.Studio.EventInstance soundEvent;

    // Start is called before the first frame update
    void Start()
    {
       
        elevator = this.gameObject.GetComponent<ElevatorMover>();

        //soundEvent = FMODUnity.RuntimeManager.CreateInstance(Elevator);
        //soundEvent.start();
        //soundEvent.setParameterByName("ElevatorP", 1);



    }

    // Update is called once per frame
    void Update()
    {

        if(elevator.elevatorMoving)
        {
            //print("Elevator Moving");
            FMODUnity.RuntimeManager.PlayOneShot(Elevator, GetComponent<Transform>().position);
            //soundEvent.setParameterByName("ElevatorP", 0f);
        }
        //else
        //{
           
        //    soundEvent.setParameterByName("ElevatorP", 1f);
        //}
       
    }
}
