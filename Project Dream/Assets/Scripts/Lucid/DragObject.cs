﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DragObject : MonoBehaviour
{
    GameObject ComponentUI;

    private Vector3 mouseOffset;

    private float mouseCoordZ;

    Vector3 transPosition;

    GameObject platform;

    public float movementRange;

    void Start()
    {
        ComponentUI = GameObject.Find("UI_Icon");

        platform = gameObject.transform.parent.gameObject;
        transPosition = platform.transform.position;
    }

    private void OnMouseDown()
    {
        mouseCoordZ = Camera.main.WorldToScreenPoint(platform.transform.position).z;

        mouseOffset = platform.transform.position - GetMouseWorldPos();
    }

    private void OnMouseDrag()
    {
        //platform.transform.position = GetMouseWorldPos() + mouseOffset;

        if (ComponentUI.GetComponent<LucidState>().isLucid)
        {
            if (gameObject.name.EndsWith("X")
            && (platform.transform.position.x <= transPosition.x + movementRange && platform.transform.position.x >= transPosition.x - movementRange))
            {
                platform.transform.position = new Vector3(GetMouseWorldPos().x + mouseOffset.x, platform.transform.position.y, GetMouseWorldPos().z + mouseOffset.z);

                if (platform.transform.position.x > transPosition.x + movementRange)
                {
                    platform.transform.position = new Vector3(transPosition.x + movementRange, platform.transform.position.y, platform.transform.position.z);
                }
                if (platform.transform.position.x < transPosition.x - movementRange)
                {
                    platform.transform.position = new Vector3(transPosition.x - movementRange, platform.transform.position.y, platform.transform.position.z);
                }
            }
            else if (gameObject.name.EndsWith("Y")
                 && (platform.transform.position.y <= transPosition.y + movementRange && platform.transform.position.y >= transPosition.y - movementRange))
            {
                platform.transform.position = new Vector3(platform.transform.position.x, GetMouseWorldPos().y + mouseOffset.y, GetMouseWorldPos().z + mouseOffset.z);

                if (platform.transform.position.y > transPosition.y + movementRange)
                {
                    platform.transform.position = new Vector3(platform.transform.position.x, transPosition.y + movementRange, platform.transform.position.z);
                }
                if (platform.transform.position.y < transPosition.y - movementRange)
                {
                    platform.transform.position = new Vector3(platform.transform.position.x, transPosition.y - movementRange, platform.transform.position.z);
                }
            }
            else if (gameObject.name.EndsWith("Any")
                 && (platform.transform.position.x <= transPosition.x + movementRange && platform.transform.position.x >= transPosition.x - movementRange)
                 && (platform.transform.position.y <= transPosition.y + movementRange && platform.transform.position.y >= transPosition.y - movementRange))
            {
                platform.transform.position = GetMouseWorldPos() + mouseOffset;

                if (platform.transform.position.x > transPosition.x + movementRange)
                {
                    platform.transform.position = new Vector3(transPosition.x + movementRange, platform.transform.position.y, platform.transform.position.z);
                }
                if (platform.transform.position.x < transPosition.x - movementRange)
                {
                    platform.transform.position = new Vector3(transPosition.x - movementRange, platform.transform.position.y, platform.transform.position.z);
                }
                if (platform.transform.position.y > transPosition.y + movementRange)
                {
                    platform.transform.position = new Vector3(platform.transform.position.x, transPosition.y + movementRange, platform.transform.position.z);
                }
                if (platform.transform.position.y < transPosition.y - movementRange)
                {
                    platform.transform.position = new Vector3(platform.transform.position.x, transPosition.y - movementRange, platform.transform.position.z);
                }
            }
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mouseCoordZ;

        return Camera.main.ScreenToViewportPoint(mousePoint);
    }
}