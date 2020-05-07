using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceType : MonoBehaviour
{

    enum SurfaceTypes { Stone, Carpet, Wood};
    
    [SerializeField]
    SurfaceTypes thisSurface;

    

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
        if(thisSurface == SurfaceTypes.Stone)
        {
            return "Stone";
        }
        else if (thisSurface == SurfaceTypes.Carpet)
        {
            return "Carpet";
        }
        else if (thisSurface == SurfaceTypes.Wood)
        {
            return "Wood";
        }

        return "idk";
    }
}
