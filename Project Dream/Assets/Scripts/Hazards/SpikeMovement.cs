using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeMovement : MonoBehaviour
{
    SpikeController spikeController;

    Transform[] waypoints = new Transform[2];

    int waypointIndex = 0;

    float speedOut;

    float speedIn;

    float moveSpeed;

    public int direction; // 0=out 1=in

    // Start is called before the first frame update
    void Start()
    {
      
    }

    private void Awake()
    {
        spikeController = gameObject.GetComponentInParent<SpikeController>();

        waypoints[0] = spikeController.Tip;
        waypoints[1] = spikeController.Wall;

        speedOut = spikeController.SpeedOut;
        speedIn = spikeController.SpeedIn;

        transform.position = waypoints[waypointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if(transform.position == waypoints[0].transform.position)
        {
            moveSpeed = speedIn;
            direction = 0;
        }
        else if (transform.position == waypoints[1].transform.position)
        {
            moveSpeed = speedOut;
            direction = 1;
        }
        else { direction = 2; }

        transform.position = Vector3.MoveTowards(transform.position, waypoints[waypointIndex].transform.position, moveSpeed * Time.deltaTime);

        if (transform.position == waypoints[waypointIndex].transform.position)
        {
            waypointIndex += 1;
        }

        if (waypointIndex == waypoints.Length)
            waypointIndex = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject playerDamageArea = other.gameObject;

        if (other.gameObject.tag == "Hazard")
        {
            BoxCollider2D playerDamage = playerDamageArea.GetComponentInParent<BoxCollider2D>();
            playerDamage.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject playerDamageArea = other.gameObject;

        if (other.gameObject.tag == "Hazard")
        {
            BoxCollider2D playerDamage = playerDamageArea.GetComponentInParent<BoxCollider2D>();
            playerDamage.enabled = false;
        }
    }
}
