using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager_Mobile : MonoBehaviour
{
    SpriteRenderer testSprite;
    Vector2 startPosition;

    float minimumSwipe = 30f;
    void Start()
    {
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

                if (touch.phase == TouchPhase.Began) {
                    startPosition = touch.position;
                }

                if (touch.phase == TouchPhase.Moved) {
                    if (touch.position.y < startPosition.y - minimumSwipe) {
                        testSprite.color = Color.green;
                    }
                }

                else if(touch.phase == TouchPhase.Ended) {
                testSprite.color = Color.red;
                }
            }

           
        }
    }

}
