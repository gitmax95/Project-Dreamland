using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpInteract : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PickUp")
        {
            gameObject.GetComponent<PlayerState>().hasSpikedShoes = true;
            Destroy(other.gameObject);
        } 
    }
}
