using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour
{
    public int playerHealth = 10;
    //public float invincibilityPeriod = 5.0f;

    float timer = 0.0f;
    // Start is called before the first frame update
    GameObject damageIndicator;
    void Start()
    {
        damageIndicator = GameObject.Find("DamageIndicator");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Hazard" && playerHealth > 0)
        {
            Debug.Log("Auch");
            playerHealth = playerHealth - collision.gameObject.GetComponent<HazardSystem>().hazardDamage;
            //playerHealth = 10;
            //timer += Time.deltaTime;
            damageIndicator.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f);
        }
        else
        {
            damageIndicator.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Hazard")
        {
            //Debug.Log("Hurts!");
            //playerHealth = playerHealth - collision.gameObject.GetComponent<HazardSystem>().hazardDamage;
            damageIndicator.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f);
        }
        else
        {
            damageIndicator.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f);
        }
    }
}
