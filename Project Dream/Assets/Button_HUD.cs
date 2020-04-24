using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_HUD : MonoBehaviour
{
    public GameObject buttonEvent;

    private void Update()
    {
        if (buttonEvent.activeInHierarchy) {
            Time.timeScale = 0f;
        } else {
            Time.timeScale = 1f;
        }
    }

    public void BackButtonPress()
    {
        buttonEvent.SetActive(true);
    }

  public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void BackButtonNO()
    {
        buttonEvent.SetActive(false);
    }
}
