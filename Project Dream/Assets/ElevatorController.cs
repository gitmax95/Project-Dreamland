using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    ElevatorMover elevatorMover;
    public ElevatorTrigger elevatorTrigger;

    bool elevatorLowered;
    int elevatorCalled;

    public float loweredDistance;
    public float plateSpeed;

    float targetPositionY;
    float startPositionY;

    bool weightPlateActivated;

    public GameObject keyObject;

    void Start()
    {
        elevatorMover = GameObject.Find("ElevatorBase").GetComponent<ElevatorMover>();

        startPositionY = transform.position.y;
        targetPositionY = transform.position.y - loweredDistance;
        
    }

   
    void Update()
    {
       

        if (weightPlateActivated) {

            if(transform.position.y > targetPositionY) {
                transform.Translate(0, plateSpeed * Time.deltaTime * -1, 0);
            } else {
                elevatorLowered = true;

                if(elevatorCalled == 0) {
                    elevatorMover.CallElevator();
                    elevatorCalled = 1;
                }
            }
        } else if(transform.position.y < startPositionY) {
            elevatorLowered = false;

            if(elevatorCalled == 1) {

                elevatorMover.CallElevator();
                elevatorCalled = 0;
            }

            transform.Translate(0, plateSpeed * Time.deltaTime * 1, 0);
        }


    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Lower Weight Plate - When lowered call Next Destination
        if (collision.gameObject.tag == keyObject.tag) {
            weightPlateActivated = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Raise Weight Plate - Call Next Destination
        if(collision.gameObject.tag == keyObject.tag) {
            weightPlateActivated = false;
        }
    }
}
