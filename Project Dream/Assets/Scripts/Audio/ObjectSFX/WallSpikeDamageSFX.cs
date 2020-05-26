using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpikeDamageSFX : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string spikeDamage = "event:/SFX/SpikeDamage";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            FMODUnity.RuntimeManager.PlayOneShot(spikeDamage, GetComponent<Transform>().position);
        }

    }
}
