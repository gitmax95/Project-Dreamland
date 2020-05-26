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

    //Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        rb.AddForce(startSpeed, 0, 0, ForceMode.Impulse);

    }

    //Update is called once per frame
    void Update()
    {
        SwingLeft();

        if (UnityEditor.TransformUtils.GetInspectorRotation(gameObject.transform).x >= max)
        {
            ForceLeftActive = false;
            StartCoroutine(MaxLeftReached());
        }
        else if (UnityEditor.TransformUtils.GetInspectorRotation(gameObject.transform).x <= -max)
        {
            ForceRightActive = false;
            StartCoroutine(MaxRightReached());
        }

        }

    private void SwingLeft()
    {

        if (UnityEditor.TransformUtils.GetInspectorRotation(gameObject.transform).x >= 0/*transform.rotation.eulerAngles.x >= 0 && transform.rotation.eulerAngles.x <= max*/)
        {
            if (ForceLeftActive)
            {
                rb.AddForce(-force, 0, 0, ForceMode.Force);
            }

        }

        if(UnityEditor.TransformUtils.GetInspectorRotation(gameObject.transform).x <= 0 /*transform.rotation.eulerAngles.x <= 0 && transform.rotation.eulerAngles.x >= -max*/)
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

