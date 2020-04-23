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

}
