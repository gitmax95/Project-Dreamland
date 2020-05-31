using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneType : MonoBehaviour
{

    [SerializeField]
    public int type;
    [Header("Values between 10 and 1 ONLY")]
    public int fadeSeconds;

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
        return fadeSeconds;
    }
}
