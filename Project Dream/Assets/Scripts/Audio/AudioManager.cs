using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    PlayerState playerState;

    [FMODUnity.EventRef]
    public string Jump = "event:/Music/Jumping";

    [FMODUnity.EventRef]
    public string Land = "event:/Music/Landing";

   
   
    // Start is called before the first frame update
    void Start()
    {

        playerState = GameObject.Find("PlayerChar").GetComponent<PlayerState>();


    }

    // Update is called once per frame
    void Update()
    {
            

    }

    void PlayJump()
    {
       
    }

    void PlayLand()
    {
        
    }

    void PlayWalk()
    {

    }

    void PlaySlide()
    {

    }


}
