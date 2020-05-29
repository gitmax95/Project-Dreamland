using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingSpikesSFX : MonoBehaviour
{
    SpikeMovementDown spikeCeiling;

    Transform spike;

    [FMODUnity.EventRef]
    string SpikeCeiling = "event:/SFX/SpikeCeiling";

    [FMODUnity.EventRef]
    public string spikeDamage = "event:/SFX/SpikeDamage";

    [FMODUnity.EventRef]
    public string spikeGroundDamage = "event:/SFX/SpikeGroundDamage";

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


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            FMODUnity.RuntimeManager.PlayOneShot(spikeDamage, GetComponent<Transform>().position);
        }
        else if (collision.gameObject.tag == "Ground")
        {
            print("something");
            FMODUnity.RuntimeManager.PlayOneShot(spikeGroundDamage, GetComponent<Transform>().position);
            Rigidbody2D rb = this.gameObject.GetComponent<Rigidbody2D>();
            rb.isKinematic = true;
        }
    }

   
}
