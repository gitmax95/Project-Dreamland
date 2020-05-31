using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class TutorialEvent : MonoBehaviour
{
   
    enum TutorialEvents {jump, slide, comboJump };

    [SerializeField]
    TutorialEvents thisEvent;

    bool tutorialCompleted;
    int tutorialStep;

    bool insideEvent;
    public float eventDelay;
    public float minimumDuration;

    bool eventCompleted;

    public GameObject eventObject;
    Transform secondPosition;
    Transform thirdPosition; 

    float delayTimer;
    float durationTimer;

    int comboStep;

    public Animator tutorialAnimator;

    PlayerState playerState;

    private void Start()
    {
        playerState = GameObject.Find("PlayerChar").GetComponent<PlayerState>();

        minimumDuration += eventDelay;

        secondPosition = GameObject.Find("Tutorial_Position2").GetComponent<Transform>();

        thirdPosition = GameObject.Find("Tutorial_Position3").GetComponent<Transform>();

    }

    private void Update()
    {
        
        if(tutorialStep >= 3) {
            tutorialCompleted = true;
            Time.timeScale = 1f;
            //Destroy(this.gameObject, 0.5f);
        }

        if (eventCompleted && durationTimer >= minimumDuration) {

            eventObject.SetActive(false);
            comboStep = 0;
           
        }

        if (playerState.isSliding == false) {
            comboStep = 0;
        }



        if (eventObject.activeSelf) {

            durationTimer += Time.deltaTime;

            if (thisEvent == TutorialEvents.comboJump && playerState.isSliding) {
                Time.timeScale = 0.5f;
                tutorialAnimator.speed = 4f;
            } else {
                Time.timeScale = 1f;
                tutorialAnimator.speed = 1f;
            }

            if (playerState.isSliding && tutorialAnimator.GetInteger("eventNumber") == 2) {

                comboStep = 1;
                

            }

        
            switch (thisEvent) {
                case TutorialEvents.jump: //eventNumber 1
                    if (playerState.isJumping == true) {
                        eventCompleted = true;
                        tutorialStep = 1;
                        transform.position = secondPosition.position;
                        thisEvent = TutorialEvents.slide;
                    }
                    return;

                case TutorialEvents.slide: //eventNumber 2
                    if (playerState.isSliding) {
                        eventCompleted = true;
                        tutorialStep = 2;
                        transform.position = thirdPosition.position;
                        eventObject.SetActive(false);
                        thisEvent = TutorialEvents.comboJump;
                    } else if(playerState.isRunning == false) {
                        eventObject.SetActive(false);
                    } else if(playerState.isRunning == true && !eventObject.activeSelf) {
                        eventObject.SetActive(true);
                    }
                    return;

                case TutorialEvents.comboJump: //eventNumber 3

                    if (comboStep == 1) {
                        tutorialAnimator.SetInteger("eventNumber", 1);
                    } else if (playerState.isRunning == false) {
                        eventObject.SetActive(false);
                    } else if (playerState.isRunning == true && !eventObject.activeSelf) {
                        eventObject.SetActive(true);
                    }

                    if (comboStep == 1 && playerState.isJumping) {
                       // eventCompleted = true;
                        tutorialStep = 3;
                     
                    }
                    return;

            }

        }


        if (insideEvent) {
            eventCompleted = false;

            if (!eventObject.activeSelf) {
            delayTimer += Time.deltaTime;
            }

            switch (thisEvent) 
            {
                case TutorialEvents.jump: //eventNumber 1
                    JumpEvent(eventDelay);
                   
                    return;

                case TutorialEvents.slide: //eventNumber 2
                    SlideEvent(eventDelay);
                    
                    return;

                case TutorialEvents.comboJump: //eventNumber 3
                    ComboEvent(eventDelay);
                    return;
            }

            
            
        }
  

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player") {
            insideEvent = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
       if(collision.gameObject.tag == "Player") {
            insideEvent = false;
       } 
    }

    private void JumpEvent(float delay)
    {
        float thisDelay = delay;

        if(delayTimer >= thisDelay && !tutorialCompleted) {
            eventObject.SetActive(true);

            if (!tutorialAnimator.IsInTransition(0)) {
                //tutorialAnimator.SetInteger("eventNumber", 1);
                tutorialAnimator.Play("Tutorial_Jump");
            }
        }    

    }

    private void SlideEvent(float delay)
    {
        float thisDelay = delay;

        if (delayTimer >= thisDelay && !tutorialCompleted) {
            if (playerState.isRunning) {
                eventObject.SetActive(true);

                if (!tutorialAnimator.IsInTransition(0)) {
                    tutorialAnimator.SetInteger("eventNumber", 2);
                    //tutorialAnimator.Play("Tutorial_Swipe");
                }
            }

        }

    }

    private void ComboEvent(float delay)
    {
        float thisDelay = delay;

        if (delayTimer >= thisDelay && !tutorialCompleted) {
            if (playerState.isRunning) {
                eventObject.SetActive(true);
            }

            if(comboStep == 0) {
                tutorialAnimator.SetInteger("eventNumber", 2);
            }
                    
        }

    }

}
