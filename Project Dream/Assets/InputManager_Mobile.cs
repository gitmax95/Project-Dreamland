using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager_Mobile : MonoBehaviour
{
    ControllerStates controllerStates;
    SpriteRenderer testSprite;

    Vector2 startPosition;
    public bool fingerMoved;

    float minimumSwipe = 30f;
    void Start()
    {
        controllerStates = GameObject.Find("InputManager").GetComponent<ControllerStates>();

        testSprite = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount == 0) {
            testSprite.color = Color.white;
        }

        foreach (Touch touch in Input.touches) {

            if (touch.position.x > Screen.width / 2) { //Checks if touch happens on right side of screen.

                if (touch.phase == TouchPhase.Began) { //Touch Began on right side
                    startPosition = touch.position;

                }

                if (touch.phase == TouchPhase.Moved) { //Touch moved
                    
                        if (touch.position.y < startPosition.y - minimumSwipe) { //Player Down Swiped Screen    
                        fingerMoved = true;
                        controllerStates.rightFinger = ControllerStates.FingerState.downSwipe;
                        }
                }
                if (touch.phase == TouchPhase.Ended && fingerMoved) {

                    fingerMoved = false;
                    controllerStates.rightFinger = ControllerStates.FingerState.noTouch; //need to check for if a tap was made as well! This will always overwrite a fingerState.tap - only use notouch if a swipe was made and ended

                }

                else if (touch.phase == TouchPhase.Ended && fingerMoved == false) { //Player Tapped Screen
               
                     controllerStates.rightFinger = ControllerStates.FingerState.tap;

                }
              

            }
          
        }
       if (controllerStates.rightFinger == ControllerStates.FingerState.tap) {
            testSprite.color = Color.green;
        }

        if (controllerStates.rightFinger == ControllerStates.FingerState.downSwipe) {
            testSprite.color = Color.red;
        } 

    }

}
