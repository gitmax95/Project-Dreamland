using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideGapBehavior : MonoBehaviour
{
    BasicMovement_Player playerMovementScript;
    PlayerState playerStateScript;

    public float slideIncrement;
    public float initialSlideDuration;
    // Start is called before the first frame update
    void Start()
    {
        playerMovementScript = GameObject.Find("PlayerChar").GetComponent<BasicMovement_Player>();
        playerStateScript = GameObject.Find("PlayerChar").GetComponent<PlayerState>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            if (playerStateScript.isSliding)
            {
                playerMovementScript.slideDuration = playerMovementScript.slideDuration + slideIncrement;
            }            
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            if (playerStateScript.isSliding)
            {
                playerMovementScript.slideDuration = playerMovementScript.slideDuration + slideIncrement;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            if (playerStateScript.isSliding)
            {
                playerMovementScript.slideDuration = initialSlideDuration;
            }
        }
    }
}
