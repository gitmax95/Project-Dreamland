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
   
    void Start()
    {
        startPosition = this.transform.position;
        playerChar = GameObject.Find("PlayerChar");
    }


    void Update()
    { 

        if (Input.GetKey(KeyCode.E)) { //Player triggered elevator movement.
            destination = direction;
        }

        if (transform.position.y <= targetTransform.position.y) { //Elevator reached Target position or slightly below. - Change next Direction
            direction = 1; 
            
            if(direction != destination) { 
                transform.position = new Vector3(transform.position.x, targetTransform.position.y, transform.position.z);
            }
            
        } else if(transform.position.y >= startPosition.y) { //Elevator is at Origin Position or slightly above. - Change next Direction.
            direction = -1;

            if (direction != destination) {
                transform.position = new Vector3(transform.position.x, startPosition.y, transform.position.z);
            }
        }


        if (direction == destination) { //Makes Elevator Move
            transform.Translate(0f, elevatorSpeed * direction * Time.deltaTime, 0f);
        }

        if (playerOnboard) {
            playerChar.transform.parent = this.transform;
        } else {
            playerChar.transform.parent = null;
        }

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
