using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour
{
    PlayerState playerStateScript;
    PlayerPosition gameStateManagerScript;

    float timer = 0.0f;
    float timerDying = 0.0f;

    public float deathDuration;
    public int playerHealth = 10;
    public GameObject onDeath;
    // Start is called before the first frame update
    void Start()
    {
        playerStateScript = GameObject.Find("PlayerChar").GetComponent<PlayerState>();
        gameStateManagerScript = GameObject.Find("PlayerChar").GetComponent<PlayerPosition>();
    }

    // Update is called once per frame
    void Update()
    {
        DeathTimer();
    }

    private void DeathTimer()
    {
        if (playerStateScript.isDying)
        {
            timerDying += Time.deltaTime;
            if (timerDying > deathDuration) //<- Hardcoded and must be adjusted for the length of Dying Animation!
            {
                onDeath.SetActive(true);
                //gameStateManagerScript.RestartGame();
            }
        }
    }
}
