using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextDeactivate : MonoBehaviour
{
    float timer;
    public float activeDuration;

    // Update is called once per frame
    void Update()
    {

        if (this.gameObject.activeSelf) {

            timer += Time.deltaTime;

            if(timer >= activeDuration) {
                timer = 0f;
                this.gameObject.SetActive(false);
            }
        }
    }
}
