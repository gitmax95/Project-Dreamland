using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationEvent : MonoBehaviour
{

    public ParticleSystem activationParticle;
    CheckpointSystem thisCheckPoint;
    GameMaster gameMasterScript;

    bool particleAllowed;
    bool checkPoint;

    private void Start()
    {
        
        if(this.gameObject.name != " AbilityUnlock") {
            thisCheckPoint = GetComponent<CheckpointSystem>();
            gameMasterScript = GameObject.Find("GameStateManager").GetComponent<GameMaster>();
            checkPoint = true;

        } else {
            checkPoint = false;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (checkPoint) {

            if (gameMasterScript.lastCheckpointPos == this.transform.position) {
                particleAllowed = false;
            } else if (gameMasterScript.lastCheckpointPos != this.transform.position) {
                particleAllowed = true;
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && particleAllowed) {
            activationParticle.Play();
        }
    }
}
