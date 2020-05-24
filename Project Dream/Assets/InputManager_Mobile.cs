using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager_Mobile : MonoBehaviour
{
    ControllerStates controllerStates;
    PlayerState playerState;
    SpriteRenderer testSprite;

    Vector2 startPosition;

    public bool fingerMoved;

    float minimumSwipe = 30f;

    Touch fingerTouch;

    private bool IsPointerOverUIObject(Vector2 touchPos)
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(touchPos.x, touchPos.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    void Start()
    {
        controllerStates = GameObject.Find("InputManager").GetComponent<ControllerStates>();
        playerState = GameObject.Find("PlayerChar").GetComponent<PlayerState>();

        testSprite = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount == 0) {
            testSprite.color = Color.white;
        }
      
        foreach (Touch touch in Input.touches) {

            if (touch.position.x > Screen.width / 2 && !IsPointerOverUIObject(touch.position)) { //Checks if touch happens on right side of screen.

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
            else if (touch.position.x > Screen.width / 2 && IsPointerOverUIObject(touch.position)) {
                controllerStates.rightFinger = ControllerStates.FingerState.noTouch;
            }
          
        }

        if (playerState.jumpActivated && controllerStates.rightFinger == ControllerStates.FingerState.tap) { //Player is done jumping but FingerState is still "tap"
            controllerStates.rightFinger = ControllerStates.FingerState.noTouch; //Reset FingerState after a jump
        }

        //DEBUG INPUT with TestSprite
        if (controllerStates.rightFinger == ControllerStates.FingerState.tap) {
            testSprite.color = Color.green;
        }

        if (controllerStates.rightFinger == ControllerStates.FingerState.downSwipe) {
            testSprite.color = Color.red;
        } 

    }

}
