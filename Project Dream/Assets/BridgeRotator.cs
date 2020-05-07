using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeRotator : MonoBehaviour {

    bool rotateBridge;
    float timeCount;
    float timeCount_Handle;
    public float rotationSpeed_Bridge;
    public float rotationSpeed_Handle;

    bool raiseBridge;
    bool bridgeRaised;
    bool lowerBridge;
    bool bridgeLowered = true;

    public Transform bridgeRotation;
    Quaternion targetRotation;

    public Transform handleRotation;
    Quaternion targetRotation_Handle;

    //public BoxCollider2D invisibleBridgeWall;
    public BoxCollider2D bridgeFloor;
    //public BoxCollider2D bridgeZoomZone;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if(rotateBridge && bridgeLowered) { //PLAYER ATTEMPTS TO RAISE BRIDGE WHEN ALLOWED
            raiseBridge = true;          
        }
        if(rotateBridge && bridgeRaised) { //PLAYER ATTEMPTS TO LOWER BRIDGE WHEN ALLOWED
            lowerBridge = true;
        }

        if (raiseBridge) { //Raising Bridge

            targetRotation_Handle.eulerAngles = new Vector3(90, -30, 0);
            handleRotation.localRotation = Quaternion.Slerp(handleRotation.localRotation, targetRotation_Handle, timeCount_Handle); //Rotating Lever Handle

            timeCount_Handle += Time.deltaTime * rotationSpeed_Handle;

            //////////////////////////////
            
            targetRotation.eulerAngles = new Vector3(0, 90, 0);

            bridgeRotation.localRotation = Quaternion.Slerp(bridgeRotation.localRotation, targetRotation, timeCount);

            timeCount += Time.deltaTime * rotationSpeed_Bridge;

            if(Quaternion.Angle(bridgeRotation.localRotation, targetRotation) == 0) {
                bridgeRotation.localRotation = targetRotation;
                bridgeRaised = true;
                bridgeLowered = false;
            }

            if (bridgeRaised) {
                rotateBridge = false;
                raiseBridge = false;
                timeCount = 0f;
                timeCount_Handle = 0f;
            }
        }

        if (lowerBridge) { //Lowering Bridge

            targetRotation_Handle.eulerAngles = new Vector3(90, 0, 0);
            handleRotation.localRotation = Quaternion.Slerp(handleRotation.localRotation, targetRotation_Handle, timeCount_Handle); //Rotating Lever Handle

            timeCount_Handle += Time.deltaTime * rotationSpeed_Handle;

            //////////////////////////////

            targetRotation.eulerAngles = new Vector3(0, 0, 0);

            bridgeRotation.localRotation = Quaternion.Slerp(bridgeRotation.localRotation, targetRotation, timeCount);

            timeCount += Time.deltaTime * rotationSpeed_Bridge;

            if(Quaternion.Angle(bridgeRotation.localRotation, targetRotation) == 0) {
                bridgeRotation.localRotation = targetRotation;
                bridgeLowered = true;
                bridgeRaised = false;
            }

            if (bridgeLowered) {
                rotateBridge = false;
                lowerBridge = false;
                timeCount = 0f;
                timeCount_Handle = 0f;
                //invisibleBridgeWall.enabled = false;
                bridgeFloor.enabled = true;
               // bridgeZoomZone.enabled = true;
            }
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.F)) {
            UseLever(collision);
        }
    }

    public void UseLever(Collider2D collision)
    {
        //if (collision.tag == "Player") {  //Player tries to activate Lever        

            if (bridgeLowered || bridgeRaised) { //If bridge ready. Lever Activated             
                rotateBridge = true;
                //invisibleBridgeWall.enabled = true;
                bridgeFloor.enabled = false;
                //bridgeZoomZone.enabled = false;

            }


        //}
    }
}
