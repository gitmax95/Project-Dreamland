using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideWallManager : MonoBehaviour
{
    PlayerState playerState;
    public BoxCollider2D wallCollider;

    void Start()
    {
        if(GameObject.Find("PlayerChar") != null) {
            playerState = GameObject.Find("PlayerChar").GetComponent<PlayerState>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (playerState.isSliding) {
            wallCollider.enabled = false;
        } else {
            wallCollider.enabled = true;
        }
    }
}
