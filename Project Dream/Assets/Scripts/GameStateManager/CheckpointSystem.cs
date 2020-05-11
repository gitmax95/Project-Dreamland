using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointSystem : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene("LevelTestLudwig");
    }
}


   

