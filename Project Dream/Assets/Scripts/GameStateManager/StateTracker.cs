using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateTracker : MonoBehaviour
{
    bool isBridgeDown;
    bool isElevatorDown;
    bool isLucidActive;
    int lucidCharges;

    void Start()
    {
        isBridgeDown = false;
        isElevatorDown = false;
        isLucidActive = false;
    }

    void Update()
    {
        
    }
}
