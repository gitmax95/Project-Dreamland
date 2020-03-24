using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrictionManager_Ground : MonoBehaviour
{
    BoxCollider2D groundCollider;

    public PhysicsMaterial2D landingMaterial;
    public PhysicsMaterial2D frictionMaterial;
    void Start()
    {
        groundCollider = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag== "Player") {
            groundCollider.sharedMaterial = frictionMaterial;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player") {
            groundCollider.sharedMaterial = landingMaterial;
        }
    }
   
}
