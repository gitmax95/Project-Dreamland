using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    PlayerHealthSystem playerHealthSystem;
    float currentHealth;

    public Image fill_HealthOrb;

    void Start()
    {
        playerHealthSystem = GameObject.Find("PlayerChar").GetComponent<PlayerHealthSystem>();

        fill_HealthOrb = GameObject.Find("Fill_HealthOrb").GetComponent<Image>();
    }


    void Update()
    {

        currentHealth = playerHealthSystem.playerHealth;

        fill_HealthOrb.fillAmount = currentHealth / 10;
    }
}
