using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteBehaviour_Player : MonoBehaviour
{
    PlayerState playerState;
    GameObject appearance;
    SpriteRenderer playerSpriteRenderer;
    CapsuleCollider2D bodyCollider;

    
    void Start()
    {
        playerState = GetComponent<PlayerState>();
        playerSpriteRenderer = GameObject.Find("Appearance").GetComponent<SpriteRenderer>();
        appearance = GameObject.Find("Appearance");

        

    }

    void Update()
    {

        //if(playerState.isFacingRight)
        //{
        //    playerSpriteRenderer.flipX = true;
            
        //}
        //else if(playerState.isFacingRight)
        //{
        //    playerSpriteRenderer.flipX = false;          
        //} 

        // ^ THIS BECAME OBSOLETE BECAUSE WHEN PLAYER TURNS, I FLIP THE WHOLE TRANSFORM

        //DEALS WITH APPEARANCE POSITION CHANGES DEPENDENT ON ANIMATION
        if (playerState.isIdle && appearance.transform.localPosition.y != -0.01f)
        {
            appearance.transform.localPosition = new Vector3(0f, 0.01f * -1, 0f);
        }
        else if (playerState.isRunning && appearance.transform.localPosition.y != -0.08f)
        {
            appearance.transform.localPosition = new Vector3(0f, -0.08f, 0f);
        }
        else if(playerState.isSliding && appearance.transform.localPosition.y != -0.01)
        {
            appearance.transform.localPosition = new Vector3(0f, 0.01f * -1, 0f);
        }

      
    }
}
