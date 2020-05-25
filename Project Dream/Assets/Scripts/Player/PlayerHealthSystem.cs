using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthSystem : MonoBehaviour
{
    //public bool animationFinish;
    //public float invincibilityPeriod = 5.0f;

    PlayerDamageTracker playerDamageScript;

    GameObject playerAppearance;
    GameObject healthOrbFillUI;

    //GameObject healthOrbBorderUI;

    //Color defaultColor;
    void Start()
    {
        playerAppearance = GameObject.Find("Appearance");

        healthOrbFillUI = GameObject.Find("Fill_HealthOrb");
        //healthOrbBorderUI = GameObject.Find("Border_HealthOrb");

        playerDamageScript = GameObject.Find("PlayerChar").GetComponent<PlayerDamageTracker>();

        //defaultColor = playerAppearance.GetComponent<SpriteRenderer>().color;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Hazard" && playerDamageScript.playerHealth > 0)
        {
            playerDamageScript.playerHealth = playerDamageScript.playerHealth - collision.gameObject.GetComponent<HazardSystem>().hazardDamage; //Make public enter

            if (playerDamageScript.playerHealth < 0)
            {
                playerDamageScript.playerHealth = 0;
            }

            playerAppearance.GetComponent<SpriteRenderer>().color = Color.red;
            healthOrbFillUI.GetComponent<Image>().color = Color.red;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Hazard")
        {
            playerAppearance.GetComponent<SpriteRenderer>().color = Color.red;
            healthOrbFillUI.GetComponent<Image>().color = Color.red;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Hazard")
        {
            playerAppearance.GetComponent<SpriteRenderer>().color = Color.white;
            healthOrbFillUI.GetComponent<Image>().color = Color.white;
        }
    }


}
