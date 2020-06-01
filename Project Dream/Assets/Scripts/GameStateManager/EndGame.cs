using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public PlayerPosition gameStateManagerScript;
    public GameObject onDeathEvent;

    private void Update()
    {
        if (onDeathEvent.activeInHierarchy)
        {
            Time.timeScale = 0f;
        }
        else
        {
           Time.timeScale = 1f;
        }
    }

    public void ButtonYes()
    {
        //gameObject.SetActive(false);
        //Time.timeScale = 1.0f;
        gameStateManagerScript.RestartGame();
    }

    public void ButtonNo()
    {
        SceneManager.LoadScene("MainMenu");
    }
}


