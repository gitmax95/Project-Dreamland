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

        platform = gameObject.transform.parent.gameObject;

        startPosition = new Vector3(platform.transform.position.x, platform.transform.position.y, platform.transform.position.z);

        thisCollider = GetComponent<BoxCollider2D>();
        
        
    }

   
    void Update()
    {
        if (lucidState.isLucid) {

            /*if(platform.transform.position.x >= startPosition.x - maxRangeX && platform.transform.position.x <= startPosition.x + maxRangeX) {
                dragAllowed = true;
            }

            else if(platform.transform.position.y >= startPosition.y - maxRangeY && platform.transform.position.y <= startPosition.y + maxRangeY) {
                dragAllowed = true;
            }

            else {
                dragAllowed = false;
            }*/
            dragAllowed = true;

        }

        if(Input.touchCount > 0) {

            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            Vector2 touchPosition = new Vector2(worldPoint.x, worldPoint.y);

            if(dragAllowed && thisCollider.OverlapPoint(touchPosition)){
                platform.transform.position = new Vector3(startPosition.x + 1f, startPosition.y + 1f, startPosition.z);
            }

        }

    }
}
