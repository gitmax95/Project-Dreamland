using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeMovementDown : MonoBehaviour
{
    SpikeControllerCeiling spikeControllerCeiling;

    Transform ground;
        
    float moveSpeed;

    public bool fall = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Awake()
    {
        spikeControllerCeiling = gameObject.GetComponentInParent<SpikeControllerCeiling>();

        ground = spikeControllerCeiling.Ground;

        moveSpeed = spikeControllerCeiling.SpeedDown;
    }

    // Update is called once per frame
    void Update()
    {
        if (fall)
        {
            Movement();
        }
    }

    void Movement()
    {
        transform.position = Vector3.MoveTowards(transform.position, ground.position, moveSpeed * Time.deltaTime);

        if (transform.position == ground.position)
        {
            fall = false;
        }
    }
}
