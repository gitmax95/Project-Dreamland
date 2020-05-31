using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorMover : MonoBehaviour
{
    GameObject playerChar;
    public bool playerOnboard;

    public float elevatorSpeed;
    Vector3 startPosition;
    public Transform targetTransform;

    int direction = -1; //Next intended direction for Elevator
    int destination; //Player wants next direction to be destination.

    public bool elevatorArrived;
    public bool elevatorMoving;
   
    void Start()
    {
        startPosition = this.transform.position;
        playerChar = GameObject.Find("PlayerChar");
    }


    void Update()
    {
      

        if (Input.GetKey(KeyCode.E)) { //Developer triggered elevator movement.
            CallElevator();
        }

        if (transform.position.y <= targetTransform.position.y) { //Elevator reached Target position or slightly below. - Change next Direction
            direction = 1; 
            
            if(direction != destination) { //Elevator reached Target Position - Adjust position to be correct.
                transform.position = new Vector3(transform.position.x, targetTransform.position.y, transform.position.z);
                elevatorArrived = true;
            } else {
                elevatorArrived = false;
            }
            
        } else if(transform.position.y >= startPosition.y) { //Elevator is at Origin Position or slightly above. - Change next Direction.
            direction = -1;

            if (direction != destination) { //Elevator is at Origin Position - Adjust position to be correct.
                transform.position = new Vector3(transform.position.x, startPosition.y, transform.position.z);
                elevatorArrived = true;
            } else {
                elevatorArrived = false;
            }
        }


        if (direction == destination) { //Makes Elevator Move
            transform.Translate(0f, elevatorSpeed * direction * Time.deltaTime, 0f);
            elevatorMoving = true;
        } else {
            elevatorMoving = false;
        }

        if (playerOnboard) {
            playerChar.transform.parent = this.transform;
        } else {
            playerChar.transform.parent = null;
        }

    }

    public void CallElevator()
    {
        destination = direction;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player") {
            playerOnboard = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            playerOnboard = false;
        }
    }
}
