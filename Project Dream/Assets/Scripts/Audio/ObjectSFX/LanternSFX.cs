using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternSFX : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string Lantern = "event:/SFX/Lantern";
    // Start is called before the first frame update
    void Start()
    {
        FMODUnity.RuntimeManager.PlayOneShot(Lantern, GetComponent<Transform>().position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
