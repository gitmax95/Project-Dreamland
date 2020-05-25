using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterial : MonoBehaviour
{
    GameObject ComponentUI;

    Material PlatformNormal;
    Material PlatformLucid;
    // Start is called before the first frame update
    void Start()
    {

        PlatformNormal = Resources.Load<Material>("Materials/Platform_PlaceHolder");
        PlatformLucid = Resources.Load<Material>("Materials/Torch_PlaceHolder");
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.Find("LucidIcon") != null) {
            ComponentUI = GameObject.Find("LucidIcon");

            if (ComponentUI.activeInHierarchy) {

                if (ComponentUI.GetComponent<LucidState>().isLucid)
                {
                    gameObject.GetComponent<MeshRenderer>().material = PlatformLucid;
                }
                else if (!ComponentUI.GetComponent<LucidState>().isLucid)
                {
                    gameObject.GetComponent<MeshRenderer>().material = PlatformNormal;
                }

            }
        }

    }
}
