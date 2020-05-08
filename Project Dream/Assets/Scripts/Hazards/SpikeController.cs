using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeController : MonoBehaviour
{
    [SerializeField]
    public float SpeedOut;

    [SerializeField]
    public float SpeedIn;

    [SerializeField]
    GameObject spikeObj;

    public Transform Tip;
    public Transform Wall;

    SpikeMovement spikeWall;
   
    // Start is called before the first frame update
    void Start()
    {    
        spikeWall = spikeObj.GetComponent<SpikeMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int State()
    {
        return spikeWall.direction;
    }
}
