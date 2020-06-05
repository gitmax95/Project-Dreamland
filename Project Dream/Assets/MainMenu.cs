using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void PlayerScene()
    {
        SceneManager.LoadScene("Player_Scene");
    }

    public void BridgeScene()
    {
        SceneManager.LoadScene("LevelTestLudwig");
        //SceneManager.LoadScene("MaxWorld");
    }

    public void DarkRoomScene()
    {
        SceneManager.LoadScene("Room_DarkRoom");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void BeachRoomScene()
    {
        SceneManager.LoadScene("Room_Beach");
    }

    public void CreditsScene()
    {
        SceneManager.LoadScene("Credits_Scene");
    }

    public void MainMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void NarrativeScene()
    {
        SceneManager.LoadScene("Narrative");
    }
    
    public void EndScene()
    {
        SceneManager.LoadScene("EndScene");
    }
}
