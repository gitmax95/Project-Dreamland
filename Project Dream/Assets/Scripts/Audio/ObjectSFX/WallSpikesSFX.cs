using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpikesSFX : MonoBehaviour
{
    SpikeMovement spikeWall;

    Transform spike;

    [FMODUnity.EventRef] string SpikeWallIn = "event:/SFX/SpikeWallIn";
    [FMODUnity.EventRef] string SpikeWallOut = "event:/SFX/SpikeWallOut";

    // Start is called before the first frame update
    void Start()
    {
        spike = this.gameObject.transform;

        spikeWall = this.gameObject.GetComponent<SpikeMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (spikeWall.direction == 0)
        {
            FMODUnity.RuntimeManager.PlayOneShot(SpikeWallIn, spike.position);
        }
        else if(spikeWall.direction == 1)
        {
            FMODUnity.RuntimeManager.PlayOneShot(SpikeWallOut, spike.position);
        }
    }
}
