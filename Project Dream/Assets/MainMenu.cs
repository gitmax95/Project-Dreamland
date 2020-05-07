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
    }

    public void DarkRoomScene()
    {
        SceneManager.LoadScene("Room_DarkRoom");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
