using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerStates : MonoBehaviour
{
    public enum FingerState
    {
        noTouch,
        tap,
        downSwipe,
        buttonClick

    }

    public FingerState leftFinger;
    public FingerState rightFinger;

    public Joystick touchJoystick;
    bool mobileDevice;
    public float input_Horizontal;

    private void Update()
    {
        if(touchJoystick.Horizontal != 0) {
            mobileDevice = true;
        }

        if(mobileDevice == false) {
            input_Horizontal = Input.GetAxis("Horizontal");
        } else {
            input_Horizontal = touchJoystick.Horizontal;
        }
        
    }
}
