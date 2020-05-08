using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawController : MonoBehaviour
{

    [SerializeField]
    public float Speed;

    [SerializeField]
    GameObject sawObj;

    public Transform Left;
    public Transform Right;

    SawMovement saw;

    // Start is called before the first frame update
    void Start()
    {
        saw = sawObj.GetComponent<SawMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
