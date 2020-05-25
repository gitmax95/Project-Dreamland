using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamageTracker : MonoBehaviour
{
    PlayerHealthSystem playerHealthScript;

    GameObject playerAppearance;
    GameObject healthOrbFillUI;

    void Start()
    {
        playerAppearance = GameObject.Find("Appearance");

        healthOrbFillUI = GameObject.Find("Fill_HealthOrb");

        playerHealthScript = GameObject.Find("PlayerChar").GetComponent<PlayerHealthSystem>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Hazard" && playerHealthScript.playerHealth > 0)
        {
            playerHealthScript.playerHealth = playerHealthScript.playerHealth - collision.gameObject.GetComponent<HazardSystem>().hazardDamage; //Make public enter

            if (playerHealthScript.playerHealth < 0)
            {
                playerHealthScript.playerHealth = 0;
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
