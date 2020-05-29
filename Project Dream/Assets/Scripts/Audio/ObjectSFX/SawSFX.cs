using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawSFX : MonoBehaviour
{
   
    [FMODUnity.EventRef] string Saw = "event:/SFX/Saw";

    // Start is called before the first frame update
    void Start()
    {
        FMODUnity.RuntimeManager.PlayOneShot(Saw, GetComponent<Transform>().position);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
