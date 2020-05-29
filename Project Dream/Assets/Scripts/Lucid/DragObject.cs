using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class DragObject : MonoBehaviour
{
    GameObject player;
    GameObject ComponentUI;

    private Vector3 mouseOffset;

    private float mouseCoordZ;

    Vector3 transPosition;

    GameObject platform;

    public float movementRangeXmin;
    public float movementRangeXmax;
    public float movementRangeYmin;
    public float movementRangeYmax;

    void Start()
    {
        player = GameObject.Find("PlayerChar");
        platform = gameObject.transform.parent.gameObject;
        transPosition = platform.transform.position;
    }

    private void LateUpdate()
    {
        if(GameObject.Find("LucidIcon") != null) {
            ComponentUI = GameObject.Find("LucidIcon");
        }
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
            && (platform.transform.position.x <= transPosition.x + movementRangeXmax && platform.transform.position.x >= transPosition.x - movementRangeXmin))
            {
                platform.transform.position = new Vector3(GetMouseWorldPos().x + mouseOffset.x, platform.transform.position.y, GetMouseWorldPos().z + mouseOffset.z);

                if (platform.transform.position.x > transPosition.x + movementRangeXmax)
                {
                    platform.transform.position = new Vector3(transPosition.x + movementRangeXmax, platform.transform.position.y, platform.transform.position.z);
                }
                if (platform.transform.position.x < transPosition.x - movementRangeXmin)
                {
                    platform.transform.position = new Vector3(transPosition.x - movementRangeXmin, platform.transform.position.y, platform.transform.position.z);
                }
            }
            else if (gameObject.name.EndsWith("Y")
                 && (platform.transform.position.y <= transPosition.y + movementRangeYmax && platform.transform.position.y >= transPosition.y - movementRangeYmin))
            {
                platform.transform.position = new Vector3(platform.transform.position.x, GetMouseWorldPos().y + mouseOffset.y, GetMouseWorldPos().z + mouseOffset.z);

                if (platform.transform.position.y > transPosition.y + movementRangeYmax)
                {
                    platform.transform.position = new Vector3(platform.transform.position.x, transPosition.y + movementRangeYmax, platform.transform.position.z);
                }
                if (platform.transform.position.y < transPosition.y - movementRangeYmin)
                {
                    platform.transform.position = new Vector3(platform.transform.position.x, transPosition.y - movementRangeYmin, platform.transform.position.z);
                }
            }
            else if (gameObject.name.EndsWith("Any")
                 && (platform.transform.position.x <= transPosition.x + movementRangeXmax && platform.transform.position.x >= transPosition.x - movementRangeXmin)
                 && (platform.transform.position.y <= transPosition.y + movementRangeYmax && platform.transform.position.y >= transPosition.y - movementRangeYmin))
            {
                platform.transform.position = GetMouseWorldPos() + mouseOffset;
                


                if (platform.transform.position.x > transPosition.x + movementRangeXmax)
                {
                    platform.transform.position = new Vector3(transPosition.x + movementRangeXmax, platform.transform.position.y, platform.transform.position.z);
                }
                if (platform.transform.position.x < transPosition.x - movementRangeXmin)
                {
                    platform.transform.position = new Vector3(transPosition.x - movementRangeXmin, platform.transform.position.y, platform.transform.position.z);
                }
                if (platform.transform.position.y > transPosition.y + movementRangeYmax)
                {
                    platform.transform.position = new Vector3(platform.transform.position.x, transPosition.y + movementRangeYmax, platform.transform.position.z);
                }
                if (platform.transform.position.y < transPosition.y - movementRangeYmin)
                {
                    platform.transform.position = new Vector3(platform.transform.position.x, transPosition.y - movementRangeYmin, platform.transform.position.z);
                }
            }

            if (Mathf.Abs(player.transform.position.x - platform.transform.position.x) < 0.5f && Mathf.Abs(player.transform.position.y - platform.transform.position.y) < 0.4f
                       /*&& player.transform.position.y - platform.transform.position.y < 0.5f
                       && player.transform.position.y - platform.transform.position.y > 0.5f*/)

                       // player pos = 1
                      //player platform = 1.2
                      //player platform = 0.8
            {
                if (player.transform.position.x > platform.transform.position.x)
                {
                    platform.transform.position = new Vector3(player.transform.position.x - 0.5f, platform.transform.position.y, platform.transform.position.z);
                }
                if (player.transform.position.x < platform.transform.position.x)
                {
                    platform.transform.position = new Vector3(player.transform.position.x + 0.5f, platform.transform.position.y, platform.transform.position.z);
                }
                //if (player.transform.position.y > platform.transform.position.y)
                //{
                //    platform.transform.position = new Vector3(player.transform.position.x, platform.transform.position.y + 0.5f, platform.transform.position.z);
                //}
                //if (player.transform.position.y < platform.transform.position.y)
                //{
                //    platform.transform.position = new Vector3(player.transform.position.x, platform.transform.position.y - 0.5f, platform.transform.position.z);
                //}
            }
            // if abs player posx - platform posx < 0.2f -> platform pos.x = playerpos.x + 0.2f 
            // if abs player posy - platform posy < 0.5f -> platform pos.y = playerpos.y + 0.5f 
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mouseCoordZ;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
  
}
