using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawSFX : MonoBehaviour
{
    SawController sawController;

    Transform saw;

    [FMODUnity.EventRef] string Saw = "event:/SFX/Saw";

    FMOD.Studio.EventInstance soundEvent;

    // Start is called before the first frame update
    void Start()
    {
        saw = this.gameObject.transform.GetChild(1);
        sawController = this.gameObject.GetComponent<SawController>();

        soundEvent = FMODUnity.RuntimeManager.CreateInstance(Saw);
        soundEvent.start();
        soundEvent.release();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
