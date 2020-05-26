using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointSystem : MonoBehaviour
{
    private GameMaster gameMasterScript;

    private void Start()
    {
        gameMasterScript = GameObject.Find("GameStateManager").GetComponent<GameMaster>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            gameMasterScript.lastCheckpointPos = transform.position;
        }
    }
}


   

