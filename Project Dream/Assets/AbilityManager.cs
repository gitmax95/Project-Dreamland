using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public GameObject abilityObject;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player") {
            abilityObject.SetActive(true);

            Destroy(gameObject, 1f);
        }
    }
}
