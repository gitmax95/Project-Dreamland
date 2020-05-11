using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeControllerCeiling : MonoBehaviour
{
    [SerializeField]
    public float SpeedDown;

    [SerializeField]
    public GameObject spikeObj;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!spikeM.fall)
        {
            if (collision.gameObject.tag == "Player")
            {
                spikeM.fall = true;
            }
        }
        else
        {
            this.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
