using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonShooting : MonoBehaviour
{
    public Transform shootingPoint;
    public GameObject canonBall;

    public float Timer = 1f;

    [FMODUnity.EventRef]
    public string canonSFX = "event:/SFX/CanonBallSpawn";

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Shoot", 1f, Timer);
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    void Shoot()
    {
        Instantiate(canonBall, shootingPoint.position, shootingPoint.rotation);
        FMODUnity.RuntimeManager.PlayOneShot(canonSFX, GetComponent<Transform>().position);
    }
}
