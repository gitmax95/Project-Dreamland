using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPosition : MonoBehaviour
{
    private GameMaster gameMasterScript;
    public PlayerHealthSystem playerHealthScript;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        gameMasterScript = GameObject.Find("GameStateManager").GetComponent<GameMaster>();               
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        playerHealthScript.playerHealth = 10;
        player.transform.position = new Vector3(gameMasterScript.lastCheckpointPos.x, gameMasterScript.lastCheckpointPos.y, gameMasterScript.lastCheckpointPos.z);        
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
