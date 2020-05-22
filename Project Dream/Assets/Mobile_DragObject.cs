using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mobile_DragObject : MonoBehaviour
{
    LucidState lucidState;

    Vector3 startPosition;
    public float maxRangeY;
    public float maxRangeX;

    bool dragAllowed;

    BoxCollider2D thisCollider;

    void Start()
    {
        lucidState = GameObject.Find("LucidIcon").GetComponent<LucidState>();
        startPosition = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);

        thisCollider = GetComponent<BoxCollider2D>();
        
    }

   
    void Update()
    {
        if (lucidState.isLucid) {

            if(this.transform.position.x >= startPosition.x - maxRangeX && this.transform.position.x <= startPosition.x + maxRangeX) {
                dragAllowed = true;
            }

            else if(this.transform.position.y >= startPosition.y - maxRangeY && this.transform.position.y <= startPosition.y + maxRangeY) {
                dragAllowed = true;
            }

            else {
                dragAllowed = false;
            }

        }

        //if dragAllowed and collider overlaps with a touch, position is touchPosition.

    }
}
