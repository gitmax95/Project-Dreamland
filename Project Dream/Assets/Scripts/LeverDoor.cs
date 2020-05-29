using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverDoor : MonoBehaviour
{

    [SerializeField]
    Transform[] waypoints;

    [SerializeField]
    float moveSpeed = 2f;

    int waypointIndex = 0;

    BridgeRotator bridge;

    [FMODUnity.EventRef]
    string door = "event:/SFX/LeverDoor";

    bool playSound = false;

    void Start()
    {
        transform.position = waypoints[waypointIndex].transform.position;
        bridge = GameObject.Find("RotationController").GetComponent<BridgeRotator>();
    }


    void Update()
    {
        
        if (bridge.bridgeRaised)
        {
            Move(0);
            if(bridge.bridgeRotation)
            {
                FMODUnity.RuntimeManager.PlayOneShot(door, GetComponent<Transform>().position);
            }

        }
        else if(bridge.bridgeLowered)
        {
            Move(1);
            if (bridge.bridgeRotation)
            {
                FMODUnity.RuntimeManager.PlayOneShot(door, GetComponent<Transform>().position);
            }
        }
    }

    void Move(int destination)
    {
        transform.position = Vector2.MoveTowards(transform.position,
                                                waypoints[destination].transform.position,
                                                moveSpeed * Time.deltaTime);
       
    }
}
