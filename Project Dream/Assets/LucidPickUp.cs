using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LucidPickUp : MonoBehaviour
{

    public GameObject lucidIcon;
    LucidState lucidState;

    private void Start()
    {
        lucidState = lucidIcon.GetComponent<LucidState>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player") {
            if(lucidState.lucidCharges < 3) {
            lucidState.lucidCharges = lucidState.lucidCharges + 1;
            Destroy(gameObject);
            }
        }
    }
}
