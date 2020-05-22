using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthSystem : MonoBehaviour
{
    public bool animationFinish;
    public int playerHealth = 10;
    //public float invincibilityPeriod = 5.0f;

    float timer = 0.0f;
    float timerDying = 0.0f;
    float transparencyVar = 1.0f;

    PlayerState playerStateScript;
    PlayerPosition gameStateManagerScript; 

    GameObject damageIndicator;
    GameObject playerAppearance;

    GameObject healthOrbFillUI;
    GameObject healthOrbBorderUI;

    Color defaultColor;
    void Start()
    {
        playerAppearance = GameObject.Find("Appearance");
        damageIndicator = GameObject.Find("DamageIndicator");

        healthOrbFillUI = GameObject.Find("Fill_HealthOrb");
        healthOrbBorderUI = GameObject.Find("Border_HealthOrb");

        playerStateScript = GameObject.Find("PlayerChar").GetComponent<PlayerState>();
        gameStateManagerScript = GameObject.Find("PlayerChar").GetComponent<PlayerPosition>();

        animationFinish = false;

        defaultColor = playerAppearance.GetComponent<SpriteRenderer>().color;
    }
    
    // Update is called once per frame
    void Update()
    {
        DeathTimer();
        //DeathEffects();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Hazard" && playerHealth > 0)
        {
            playerHealth = playerHealth - collision.gameObject.GetComponent<HazardSystem>().hazardDamage; //Make public enter

            if (playerHealth < 0)
            {
                playerHealth = 0;
            }

            playerAppearance.GetComponent<SpriteRenderer>().color = Color.red;
            healthOrbFillUI.GetComponent<Image>().color = Color.red;
            healthOrbBorderUI.GetComponent<Image>().color = Color.black;

            //damageIndicator.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f);

        }
        //else
        //{
        //    playerAppearance.GetComponent<SpriteRenderer>().color = defaultColor;
        //}
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Hazard")
        {
            //Debug.Log("Hurts!");
            //playerHealth = playerHealth - collision.gameObject.GetComponent<HazardSystem>().hazardDamage;
            playerAppearance.GetComponent<SpriteRenderer>().color = Color.red;
            healthOrbFillUI.GetComponent<Image>().color = Color.red;
            //healthOrbBorderUI.GetComponent<Image>().color = Color.black;
        }
        //else
        //{
        //    damageIndicator.GetComponent<SpriteRenderer>().color = defaultColor;
        //}
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Hazard")
        {
            playerAppearance.GetComponent<SpriteRenderer>().color = Color.white;
            healthOrbFillUI.GetComponent<Image>().color = Color.white;
            //healthOrbBorderUI.GetComponent<Image>().color = Color.white;
        }
    }

    //private void DeathEffects()
    //{
    //    if (!playerStateScript.isDying && playerStateScript.isDead)
    //    {
    //        if (playerAppearance.GetComponent<SpriteRenderer>().color.a > 0)
    //        {
    //            playerAppearance.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, transparencyVar - 0.1f);
    //        }
    //        else
    //        {
    //            gameStateManagerScript.RestartGame();
    //        }
    //    }
    //}

    private void DeathTimer()
    {
        if (playerStateScript.isDying)
        {
            timerDying += Time.deltaTime;
            if (timerDying > 2.0f) //<- Hardcoded and must be adjusted for the length of Dying Animation!
            {
                gameStateManagerScript.RestartGame();
            }
        }
    }
}
