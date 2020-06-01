using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LucidAnnouncer : MonoBehaviour
{
    public GameObject abilityAnnouncer;
    public GameObject lucidInstruction;

    bool displayed;

    float timer_Announcement;
    public float announceDuration;

    float timer_Instruction;
    public float instructionDuration;

    // Update is called once per frame
    void Update()
    {
        if(abilityAnnouncer.activeSelf == false && !displayed ) {
            abilityAnnouncer.SetActive(true);           
        }

        if (abilityAnnouncer.activeSelf) {
            displayed = true;
            timer_Announcement += Time.deltaTime;
            if(timer_Announcement >= announceDuration) {
                abilityAnnouncer.SetActive(false);
            }
        } else if(displayed && abilityAnnouncer.activeSelf == false) {
            lucidInstruction.SetActive(true);
        }

        if (lucidInstruction.activeSelf) {

            timer_Instruction += Time.deltaTime;

            if(timer_Instruction >= instructionDuration) {
                lucidInstruction.SetActive(false);
            }
        }


    }
}
