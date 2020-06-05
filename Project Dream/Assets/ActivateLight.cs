using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateLight : MonoBehaviour
{

    ElevatorMover elevatorMoverScript;

    Light thisLight;
    // Start is called before the first frame update
    void Start()
    {
        thisLight = GetComponent<Light>();
        elevatorMoverScript = GameObject.Find("ElevatorBase").GetComponent<ElevatorMover>();
    }

    // Update is called once per frame
    void Update()
    {
        if(thisLight.enabled == false) {
            if(elevatorMoverScript.elevatorMoving && elevatorMoverScript.playerOnboard) {
                thisLight.enabled = true;
            }
        }
    }
}
