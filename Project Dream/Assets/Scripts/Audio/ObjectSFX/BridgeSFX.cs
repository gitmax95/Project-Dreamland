using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeSFX : MonoBehaviour
{
    [FMODUnity.EventRef]
    string Bridgesfx = "event:/SFX/Bridge";

    [FMODUnity.EventRef]
    string Leversfx = "event:/SFX/Lever";

    BridgeRotator bridgeRotator;
    GameObject bridge;

    // Start is called before the first frame update
    void Start()
    {
        bridgeRotator = GameObject.Find("RotationController").GetComponent<BridgeRotator>();
        bridge = GameObject.Find("Draw_Birdge");
    }

    // Update is called once per frame
    void Update()
    {

        if (bridgeRotator.rotateBridge)
        {
            print("Once");
            FMODUnity.RuntimeManager.PlayOneShot(Leversfx, GetComponent<Transform>().position);
            FMODUnity.RuntimeManager.PlayOneShot(Bridgesfx, bridge.GetComponent<Transform>().position);
            //StartCoroutine(Coroutine());
        }
    }

    IEnumerator Coroutine()
    {
        yield return new WaitForSeconds(10);


    }
}
