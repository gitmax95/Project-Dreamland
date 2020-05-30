using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class TutorialEvent : MonoBehaviour
{
   
    enum TutorialEvents {jump, slide, comboJump };

    [SerializeField]
    TutorialEvents thisEvent;

    public bool insideEvent;
    public float eventDelay;
    public float minimumDuration;

    bool eventCompleted;
    bool eventDone;

    public GameObject eventObject;

    float delayTimer;
    float durationTimer;


    public Animator tutorialAnimator;

    PlayerState playerState;

    private void Start()
    {
        playerState = GameObject.Find("PlayerChar").GetComponent<PlayerState>();

        minimumDuration += eventDelay;
    }

    private void Update()
    {
        if (eventCompleted && durationTimer >= minimumDuration) {
            delayTimer = 0f;
            eventObject.SetActive(false);

            Destroy(gameObject, 0.5f);
        }

        if (eventObject.activeSelf) {

            durationTimer += Time.deltaTime;

            switch (thisEvent) {
                case TutorialEvents.jump: //eventNumber 1
                    if (playerState.isJumping == true) {
                        eventCompleted = true;
                    }
                    return;

                case TutorialEvents.slide: //eventNumber 2
                    if (playerState.isSliding) {
                        eventDone = true;
                    }
                    return;

                case TutorialEvents.comboJump: //eventNumber 3
                    //NEED ADDITIONAL CHECKS

                    return;
            }
        }


        if (insideEvent) {

            if (!eventObject.activeSelf) {
            delayTimer += Time.deltaTime;
            }

            switch (thisEvent) 
            {
                case TutorialEvents.jump: //eventNumber 1
                    JumpEvent(eventDelay);
                    return;

                case TutorialEvents.slide: //eventNumber 2
                    
                    return;

                case TutorialEvents.comboJump: //eventNumber 3
                    
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

        if(delayTimer >= thisDelay && !eventCompleted) {
            eventObject.SetActive(true);

            if (!tutorialAnimator.IsInTransition(0)) {
            tutorialAnimator.SetInteger("eventNumber", 1);
            }
        }

      

    }

}
