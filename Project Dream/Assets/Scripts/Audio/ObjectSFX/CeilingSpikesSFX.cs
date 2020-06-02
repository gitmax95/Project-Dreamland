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
    private bool sound = true;

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
            if (sound)
            {
                FMODUnity.RuntimeManager.PlayOneShot(SpikeCeiling, spike.position);
                sound = false;
                StartCoroutine(SoundStop());
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            FMODUnity.RuntimeManager.PlayOneShot(spikeDamage, GetComponent<Transform>().position);
        }
       
    }

    IEnumerator SoundStop()
    {
        yield return new WaitForSecondsRealtime(10);
        //print("maxRight");
        sound = true;
    }


}
