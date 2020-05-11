using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LucidUI_Behaviour : MonoBehaviour
{
    public Image lucidMoon;

    Image lucidOrb1;
    public Sprite availableOrb1;
    public Sprite unAvailableOrb1;

    Image lucidOrb2;
    public Sprite availableOrb2;
    public Sprite unAvailableOrb2;

    Image lucidOrb3;
    public Sprite availableOrb3;
    public Sprite unAvailableOrb3;

    int currentCharges = 3;

    public float fillIncrement;
    public bool moonFilled;

    public Light playerAura;
    Color startColor;
    public Color targetColor;
    void Start()
    {
        lucidOrb1 = GameObject.Find("LucidOrb1").GetComponent<Image>();
        lucidOrb2 = GameObject.Find("LucidOrb2").GetComponent<Image>();
        lucidOrb3 = GameObject.Find("LucidOrb3").GetComponent<Image>();

        startColor = playerAura.color;
    }

    
    void Update()
    {
        DebugLucidOrb();

        LucidOrbState();

        if(lucidMoon.fillAmount > 0) {
            playerAura.color = targetColor;
        } else {
            playerAura.color = startColor;
        }
       
    }

    public void RefillMoon()
    {
        if (lucidMoon.fillAmount < 1) {
            lucidMoon.fillAmount += fillIncrement * Time.deltaTime;
        }

        if(lucidMoon.fillAmount >= 1) {
            moonFilled = true;
        } else {
            moonFilled = false;
        }
    }

    private void LucidOrbState()
    {
        if (currentCharges == 3) {

            ActiveOrb(1, true);

            ActiveOrb(2, true);

            ActiveOrb(3, true);


        } else if (currentCharges == 2) {

            ActiveOrb(1, true);

            ActiveOrb(2, true);

            ActiveOrb(3, false);

            RefillMoon(); //Used 1 Orb

        } else if (currentCharges == 1) {

            ActiveOrb(1, true);

            ActiveOrb(2, false);

            ActiveOrb(3, false);

            RefillMoon(); //Used the second Orb

        } else if (currentCharges <= 0) {

            ActiveOrb(1, false);

            ActiveOrb(2, false);

            ActiveOrb(3, false);

            RefillMoon(); //Used the Last Orb
        }
    }

    private void DebugLucidOrb()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            currentCharges = 1;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            currentCharges = 2;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            currentCharges = 3;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4)) {
            currentCharges = 0;
        }
    }

    void ActiveOrb(int orbNr, bool state)
    {
        if(orbNr == 1) {

            if(state == true) {
                lucidOrb1.sprite = availableOrb1;
            }
            else if(state == false) {
                lucidOrb1.sprite = unAvailableOrb1;
            }

        }

        if(orbNr == 2) {

            if(state == true) {
                lucidOrb2.sprite = availableOrb2;
            }
            else if(state == false) {
                lucidOrb2.sprite = unAvailableOrb2;
            }
        }

        if (orbNr == 3) {

            if (state == true) {
                lucidOrb3.sprite = availableOrb3;
            } else if (state == false) {
                lucidOrb3.sprite = unAvailableOrb3;
            }
        }

    }
}
