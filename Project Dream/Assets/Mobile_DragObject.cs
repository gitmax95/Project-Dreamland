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

    GameObject platform;

    void Start()
    {
        lucidState = GameObject.Find("LucidIcon").GetComponent<LucidState>();
        startPosition = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);

        thisCollider = GetComponent<BoxCollider2D>();
        
        platform = gameObject.transform.parent.gameObject;
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

        if(Input.touchCount > 0 && Input.touchCount < 2) {

            if(dragAllowed && thisCollider.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position))) {
                platform.transform.position = new Vector3(startPosition.x + 0.5f, startPosition.y + 0.5f, startPosition.z);
            }

        }

    }
}
