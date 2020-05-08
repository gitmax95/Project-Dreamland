using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpikesSFX : MonoBehaviour
{
    SpikeController spikeWall;

    Transform spike;

    [FMODUnity.EventRef] string SpikeWallIn = "event:/SFX/SpikeWallIn";
    [FMODUnity.EventRef] string SpikeWallOut = "event:/SFX/SpikeWallOut";

    // Start is called before the first frame update
    void Start()
    {
        spike = this.gameObject.transform.GetChild(1);

        spikeWall = this.gameObject.GetComponent<SpikeController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (spikeWall.State() == 0)
        {
            FMODUnity.RuntimeManager.PlayOneShot(SpikeWallIn, spike.position);
        }
        else if(spikeWall.State() == 1)
        {
            FMODUnity.RuntimeManager.PlayOneShot(SpikeWallOut, spike.position);
        }
    }
}
