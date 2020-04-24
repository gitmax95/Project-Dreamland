using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parameters : MonoBehaviour
{
    FMOD.Studio.EventInstance Ambient;

    // Start is called before the first frame update
    void Start()
    {
        Ambient = FMODUnity.RuntimeManager.CreateInstance("event:/Ambient/Ambient1");
        Ambient.start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            Ambient.setParameterByName("Ambience", 1f);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            Ambient.setParameterByName("Ambience", 0f);
        }
    }
}
