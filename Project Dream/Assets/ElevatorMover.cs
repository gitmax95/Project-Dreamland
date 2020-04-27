using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorMover : MonoBehaviour
{
    bool elevatorMoving = false;
    bool elevatorTriggered = false;

    public float elevatorSpeed;
    Transform originTransform;
    public Transform targetTransform;

    int direction;
   
    void Start()
    {
        originTransform = this.transform;
    }


    void Update()
    {
        if(transform.position.y == originTransform.position.y) {
            direction = -1;
        } else if (transform.position.y == targetTransform.position.y) {
            direction = 1;
        }

        if (Input.GetKey(KeyCode.E)) { //Player triggered elevator movement.
            elevatorTriggered = true;
        }

        if(transform.position != originTransform.position || transform.position != targetTransform.position) { //Elevator currently moving.
            elevatorMoving = true;
        } else {
            elevatorMoving = false;
        }

        if(elevatorMoving && transform.position.y <= targetTransform.position.y) { //Elevator reached destination.
            elevatorTriggered = false;
        }

        if (elevatorTriggered) { //Makes Elevator Move
            transform.Translate(0f, elevatorSpeed * direction * Time.deltaTime, 0f);
        }


      
    }
}
