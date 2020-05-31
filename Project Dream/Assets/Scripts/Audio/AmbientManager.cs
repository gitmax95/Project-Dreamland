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
    float lerpValue;
    bool ChangeParameter = true;

    [Header("Values between 10 and 1")]
    [SerializeField]
    public int speed; // speed in seconds

    // Start is called before the first frame update
    void Start()
    {
        soundEvent = FMODUnity.RuntimeManager.CreateInstance(Ambience);
        soundEvent.start();
    }

    // Update is called once per frame
    void Update()
    {
        if (ChangeParameter)
        {
            if (memoryValue < parameterValue)
            {
                soundEvent.setParameterByName("ZonePP", Mathf.Lerp(memoryValue, parameterValue, lerpValue));

                lerpValue = (1 / speed) * Time.deltaTime;

                if (parameterValue == Mathf.Lerp(memoryValue, parameterValue, lerpValue))
                {
                    memoryValue = parameterValue;
                    ChangeParameter = false;
                }
            }
            else if (memoryValue > parameterValue)
            {
                soundEvent.setParameterByName("ZonePP", Mathf.Lerp(parameterValue, memoryValue, lerpValue));

                lerpValue = (1 / speed) * Time.deltaTime;

                if (memoryValue == Mathf.Lerp(parameterValue, memoryValue, lerpValue))
                {
                    memoryValue = parameterValue;
                    ChangeParameter = false;
                }
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
                print("music");
                parameterValue = zone.getType();
                speed = zone.getFade();
            }
            
        }
    }
}
        