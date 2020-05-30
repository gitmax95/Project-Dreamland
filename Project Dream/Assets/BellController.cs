using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BellController : MonoBehaviour
{
   
    public float startSpeed;
    Rigidbody rb;
    public float force;
    public int max;
    public float sec;
    bool ForceRightActive = true;
    bool ForceLeftActive = false;

    BellRotationDetector bellRotation;

    //Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        rb.AddForce(startSpeed, 0, 0, ForceMode.Impulse);
        bellRotation = GameObject.Find("BellRotation").GetComponent<BellRotationDetector>();

    }

    //Update is called once per frame
    void Update()
    {
        SwingLeft();

        if (bellRotation.leftMax)
        {
            ForceLeftActive = false;
            StartCoroutine(MaxLeftReached());
        }
        else if (bellRotation.rightMax)
        {
            ForceRightActive = false;
            StartCoroutine(MaxRightReached());
        }

        }

    private void SwingLeft()
    {

        if (bellRotation.left)
        {

            if (ForceLeftActive)
            {
                rb.AddForce(-force, 0, 0, ForceMode.Force);
            }

        }

        if(bellRotation.right)
        {
            if (ForceRightActive)
            {
                rb.AddForce(force, 0, 0, ForceMode.Force);
            }
        }
    
    }

    IEnumerator MaxRightReached()
    {
        yield return new WaitForSecondsRealtime(sec);
        //print("maxRight");
        ForceRightActive = true;
    }

    IEnumerator MaxLeftReached()
    {
        yield return new WaitForSecondsRealtime(sec);
        //print("maxLeft");
        ForceLeftActive = true;
    }

}

