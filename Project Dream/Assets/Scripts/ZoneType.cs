using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneType : MonoBehaviour
{

    [SerializeField]
    public int type;
    public int fade;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getType()
    {
        return type;
    }

    public int getFade()
    {
        return fade;
    }
}
