using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientManager : MonoBehaviour
{

    [FMODUnity.EventRef]
    public string Ambience = "event:/Music/Ambience";

    FMOD.Studio.EventInstance soundEvent;

    int parameterValue;
    int memoryValue;
    float lerpValue;
    bool dontChangeParameter = true;

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
        if (!dontChangeParameter)
        {
            soundEvent.setParameterByName("ZonePP", Mathf.Lerp(memoryValue, parameterValue, lerpValue));

            lerpValue = (1 / speed) * Time.deltaTime;
        }

        if(lerpValue == 1)
        {
            dontChangeParameter = true;
            memoryValue = parameterValue;
        }

        if (memoryValue != parameterValue)
        {
            dontChangeParameter = false;
        }
    }

   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;

        if (other.gameObject.tag == "Zone")
        {
            ZoneType zone;
            zone = other.GetComponent<ZoneType>();

            if (zone != null)
            {
                if (zone.getType() == "Beach")
                {
                    parameterValue = 0;

                }
                else if (zone.getType() == "Bridge")
                {
                    parameterValue = 1;

                }
                else if(zone.getType() == "Tower")
                {
                    parameterValue = 2;

                }
                else if (zone.getType() == "Underground")
                {
                    parameterValue = 3;

                }
                else if (zone.getType() == "Elevator")
                {
                    parameterValue = 4;

                }

            }
            else
            {

            }
        }
    }
}
        