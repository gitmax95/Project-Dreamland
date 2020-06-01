using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AmbientManager : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string Ambience = "event:/Music/Music";

    FMOD.Studio.EventInstance soundEvent;

    int parameterValue = 0;
    int memoryValue = 0;
    float lerpValue = 0;
    float scenelerp = 0;
    float scenelerp2 = 0;
    bool ChangeParameter = true;
    bool enteredEndScene = false;

    
    int speed; // speed in seconds
    private bool destroyed = false;

    //private void Awake()
    //{
    //    int numMusicPlayers = FindObjectsOfType<AmbientManager>().Length;

    //    if (numMusicPlayers != 1)
    //    {
    //        Destroy(gameObject);
    //    }
    //    else
    //    {
    //        instance = this;
    //        DontDestroyOnLoad(this);
    //    }


    //}

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        soundEvent = FMODUnity.RuntimeManager.CreateInstance(Ambience);
        soundEvent.start();
        soundEvent.setParameterByName("EndSceneP", 1);
    }

 
    // Update is called once per frame
    void Update()
    {
        //print("par " + parameterValue);
        //print("mem " + memoryValue);

        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            soundEvent.setParameterByName("ZonePP", 0);

            if (enteredEndScene)
            {
                
                soundEvent.setParameterByName("EndSceneP", Mathf.Lerp(0, 1, scenelerp2));
                scenelerp2 += Time.deltaTime / 5;

                if (scenelerp2 > 1)
                {
                    enteredEndScene = false;
                    scenelerp2 = 0;
                }
            }
        }
        else if (SceneManager.GetActiveScene().name == "EndScene")
        {
            enteredEndScene = true;

            soundEvent.setParameterByName("EndSceneP", Mathf.Lerp(1, 0, scenelerp));
            scenelerp += Time.deltaTime / 15;

            if (scenelerp > 1)
            {
                scenelerp = 0;
            }

        }     

        


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
               // print("I reached the par");
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



    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    GameObject other = collision.gameObject;

    //    if (other.gameObject.tag == "MusicZone")
    //    {
    //        ZoneType zone;
    //        zone = other.GetComponent<ZoneType>();

    //        if (zone != null)
    //        {
    //            parameterValue = zone.getType();
    //            speed = zone.getFade();
    //        }

    //    }
    //}

    public void SetParameterValue(int a, int b)
    {
        parameterValue = a;
        speed = b;
    }
}
        