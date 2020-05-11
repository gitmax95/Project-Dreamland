using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawMovement : MonoBehaviour
{
    SawController sawController;

    Transform[] waypoints = new Transform[2];

    int waypointIndex = 0;

    float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        sawController = gameObject.GetComponentInParent<SawController>();

        waypoints[0] = sawController.Left;
        waypoints[1] = sawController.Right;

        moveSpeed = sawController.Speed;
        
        transform.position = waypoints[waypointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        transform.position = Vector3.MoveTowards(transform.position, waypoints[waypointIndex].transform.position, moveSpeed * Time.deltaTime);

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
