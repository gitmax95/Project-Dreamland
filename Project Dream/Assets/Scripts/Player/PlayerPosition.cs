using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPosition : MonoBehaviour
{
    private GameMaster gameMasterScript;
    // Start is called before the first frame update
    void Start()
    {
        gameMasterScript = GameObject.Find("GameStateManager").GetComponent<GameMaster>();
        transform.position = gameMasterScript.lastCheckpointPos;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
