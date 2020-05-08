using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeControllerCeiling : MonoBehaviour
{
    //enum SpikeType { Wall, Ceiling };

    //[SerializeField]
    //SpikeType thisSpike;

    [SerializeField]
    public float SpeedDown;

    [SerializeField]
    GameObject spikeObj;

    public Transform Ground;

    SpikeMovementDown spikeM;

    // Start is called before the first frame update
    void Start()
    {
        spikeM = spikeObj.GetComponent<SpikeMovementDown>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool State()
    {
        return spikeM.fall;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            spikeM.fall = true;
        }
    }
}
