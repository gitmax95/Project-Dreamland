using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [Tooltip("Camera follows this Transform.")]
    public Transform target;
    [Tooltip("Higher value makes Camera movement sharper.")]
    public float cameraSmoothing = 5f;

    Vector3 offset;

    void Start()
    {
        offset = transform.position - target.position; //Distance between Camera and Target Position
    }

    private void FixedUpdate()
    {

        Vector3 targetCameraPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCameraPosition, cameraSmoothing * Time.deltaTime);    //Move Camera to new Location following Target.
    }
}
