using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BellRotationDetector : MonoBehaviour
{
    public bool right;
    public bool left;
    public bool leftMax;
    public bool rightMax;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BellRight")
        {
            right = true;
        }
        else if (collision.gameObject.tag != "BellRight")
        {
            right = false;
        }

        if (collision.gameObject.tag == "BellLeft")
        {
            left = true;
        }
        else if(collision.gameObject.tag != "BellLeft")
        {
            left = false;
        }

        if (collision.gameObject.tag == "BellRightMax")
        {
            rightMax = true;
        }
        else if (collision.gameObject.tag != "BellRightMax")
        {
            rightMax = false;
        }

        if (collision.gameObject.tag == "BellLeftMax")
        {
            leftMax = true;
        }
        else if (collision.gameObject.tag != "BellLeftMax")
        {
            leftMax = false;
        }

    }
}
