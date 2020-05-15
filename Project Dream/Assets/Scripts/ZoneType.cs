using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneType : MonoBehaviour
{

    enum ZoneTypes {Beach, Bridge,Tower, UnderGround, EndGame, Elevator,  };

    [SerializeField]
    ZoneTypes thisZone;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string getType()
    {
        if (thisZone == ZoneTypes.Beach)
        {
            return "Beach";
        }
        if (thisZone == ZoneTypes.Bridge)
        {
            return "Bridge";
        }
        if (thisZone == ZoneTypes.Tower)
        {
            return "Tower";
        }
        if (thisZone == ZoneTypes.UnderGround)
        {
            return "UnderGround";
        }
        if (thisZone == ZoneTypes.Elevator)
        {
            return "Elevator";
        }


        return "idk";
    }
}
