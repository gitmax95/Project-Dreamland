using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour
{
    public bool animationFinish;
    public int playerHealth = 10;
    //public float invincibilityPeriod = 5.0f;

    float timer = 0.0f;
    float timerDying = 0.0f;
    float transparencyVar = 1.0f;

    PlayerState playerStateScript;
    CheckpointSystem gameStateManagerScript; 

    GameObject damageIndicator;
    GameObject playerAppearance;

    Color defaultColor;
    void Start()
    {
        playerAppearance = GameObject.Find("Appearance");
        damageIndicator = GameObject.Find("DamageIndicator");

        playerStateScript = GameObject.Find("PlayerChar").GetComponent<PlayerState>();
        gameStateManagerScript = GameObject.Find("GameStateManager").GetComponent<CheckpointSystem>();

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
            playerHealth = playerHealth - collision.gameObject.GetComponent<HazardSystem>().hazardDamage;

            if (playerHealth < 0)
            {
                playerHealth = 0;
            }

            playerAppearance.GetComponent<SpriteRenderer>().color = Color.red;

            //damageIndicator.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f);

        }
        else
        {
            playerAppearance.GetComponent<SpriteRenderer>().color = defaultColor;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Hazard")
        {
            //Debug.Log("Hurts!");
            //playerHealth = playerHealth - collision.gameObject.GetComponent<HazardSystem>().hazardDamage;
            playerAppearance.GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            damageIndicator.GetComponent<SpriteRenderer>().color = defaultColor;
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
