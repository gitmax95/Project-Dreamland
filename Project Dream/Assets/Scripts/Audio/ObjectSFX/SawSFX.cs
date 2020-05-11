using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawSFX : MonoBehaviour
{
    SawMovement sawM;
    Transform saw;

    [FMODUnity.EventRef] string Saw = "event:/SFX/Saw";

    // Start is called before the first frame update
    void Start()
    {
        saw = this.gameObject.transform;
        sawM = this.gameObject.GetComponent<SawMovement>();

        
    }

    // Update is called once per frame
    void Update()
    {
        FMODUnity.RuntimeManager.PlayOneShot(Saw, saw.position);
    }
}
