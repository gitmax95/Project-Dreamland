using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteBehaviour_Player : MonoBehaviour
{
    PlayerState playerState;
    SpriteRenderer playerSpriteRenderer;
    public GameObject slidePose_Left;
    public GameObject slidePose_Right;
    
    void Start()
    {
        playerState = GetComponent<PlayerState>();
        playerSpriteRenderer = GameObject.Find("Appearance").GetComponent<SpriteRenderer>();
        

    }

    void Update()
    {
        if(Input.GetAxis("Horizontal") < 0) {
            playerSpriteRenderer.flipX = true;
            

        } else if(Input.GetAxis("Horizontal") > 0) {
            playerSpriteRenderer.flipX = false;
            
        }

        if (playerState.isSliding && Input.GetAxis("Horizontal") < 0f) {
            playerSpriteRenderer.enabled = false;
            slidePose_Left.SetActive(true);
                        
            
        } else if (playerState.isSliding && Input.GetAxis("Horizontal") > 0f) {
            playerSpriteRenderer.enabled = false;
            slidePose_Right.SetActive(true);
        }
        else {
        playerSpriteRenderer.enabled = true;
            slidePose_Right.SetActive(false);
            slidePose_Left.SetActive(false);
            
        }
    }
}
