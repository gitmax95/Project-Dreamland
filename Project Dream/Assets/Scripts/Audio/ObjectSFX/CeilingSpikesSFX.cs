﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingSpikesSFX : MonoBehaviour
{
    SpikeMovementDown spikeCeiling;

    Transform spike;

    [FMODUnity.EventRef] string SpikeCeiling = "event:/SFX/SpikeCeiling";
   
    // Start is called before the first frame update
    void Start()
    {
        spike = this.gameObject.transform;

        spikeCeiling = this.gameObject.GetComponent<SpikeMovementDown>();
    }

    // Update is called once per frame
    void Update()
    {
        if(spikeCeiling.fall == true)
        {
            FMODUnity.RuntimeManager.PlayOneShot(SpikeCeiling, spike.position);
        }
    }
}