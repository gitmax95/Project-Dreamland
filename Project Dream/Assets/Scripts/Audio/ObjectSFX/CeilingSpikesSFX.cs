using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingSpikesSFX : MonoBehaviour
{
    SpikeControllerCeiling spikeCeiling;

    Transform spike;

    [FMODUnity.EventRef] string SpikeCeiling = "event:/SFX/SpikeCeiling";
   
    // Start is called before the first frame update
    void Start()
    {
        spike = this.gameObject.transform.GetChild(1);

        spikeCeiling = this.gameObject.GetComponent<SpikeControllerCeiling>();
    }

    // Update is called once per frame
    void Update()
    {
        if(spikeCeiling.State() == true)
        {
            FMODUnity.RuntimeManager.PlayOneShot(SpikeCeiling, spike.position);
        }
    }
}
