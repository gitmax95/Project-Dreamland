﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverDoor : MonoBehaviour
{

    [SerializeField]
    Transform[] waypoints;

    [SerializeField]
    float moveSpeed = 2f;

    int waypointIndex = 1;

    BridgeRotator bridge;

    [FMODUnity.EventRef]
    string door = "event:/SFX/LeverDoor";

    void Start()
    {
        transform.position = waypoints[0].transform.position;
        bridge = GameObject.Find("RotationController").GetComponent<BridgeRotator>();
    }


    void Update()
    {
        
        if (bridge.rotateBridge)
        {
            Move();
            FMODUnity.RuntimeManager.PlayOneShot(door, GetComponent<Transform>().position);

        }
        
    }

    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position,
                                                waypoints[waypointIndex].transform.position,
                                                moveSpeed * Time.deltaTime);

        if (transform.position == waypoints[waypointIndex].transform.position)
        {
            waypointIndex += 1;
        }

        if (waypointIndex == waypoints.Length)
        {
            waypointIndex = 0;
        }



    }

    
}
