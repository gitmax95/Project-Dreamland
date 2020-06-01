using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public GameObject abilityObject;
    public GameObject unlockAnnouncer;

    public float destroyAfter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player") {
            abilityObject.SetActive(true);
            unlockAnnouncer.SetActive(true);

            Destroy(gameObject, destroyAfter);
        }
    }
}
