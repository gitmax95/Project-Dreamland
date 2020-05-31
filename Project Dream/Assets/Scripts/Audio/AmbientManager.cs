using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientManager : MonoBehaviour
{

    [FMODUnity.EventRef]
    public string Ambience = "event:/Music/Music";

    FMOD.Studio.EventInstance soundEvent;

    int parameterValue = 0;
    int memoryValue = 0;
    float lerpValue = 0;
    bool ChangeParameter = true;

    
    int speed; // speed in seconds
    private bool destroyed = false;

    // Start is called before the first frame update
    void Start()
    {
        soundEvent = FMODUnity.RuntimeManager.CreateInstance(Ambience);
        soundEvent.start();
    }

 
    // Update is called once per frame
    void Update()
    {
        //print("par " + parameterValue);
        //print("mem " + memoryValue);


        if (ChangeParameter)
        {
           if(destroyed)
            {
                memoryValue = parameterValue;
                destroyed = false;
            }

                soundEvent.setParameterByName("ZonePP", Mathf.Lerp(memoryValue, parameterValue, lerpValue));

                lerpValue += Time.deltaTime/speed;

            if (parameterValue == Mathf.Lerp(memoryValue, parameterValue, lerpValue))
            {
                print("I reached the par");
                memoryValue = parameterValue;
                ChangeParameter = false;
                lerpValue = 0;
            }

        }

        if (memoryValue != parameterValue)
        {
            ChangeParameter = true;
        }
    }

   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;

        if (other.gameObject.tag == "MusicZone")
        {
            ZoneType zone;
            zone = other.GetComponent<ZoneType>();

            if (zone != null)
            {
                parameterValue = zone.getType();
                speed = zone.getFade();
            }

        }
    }

    private void OnDestroy()
    {
        print("I was destroyed");
        destroyed = true;
    }
}
        