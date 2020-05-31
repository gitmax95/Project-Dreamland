using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NarrativeText : MonoBehaviour
{
    Text narrativeText;

    public Color startColor;
    public Color midColor;
    public Color endColor;

    public GameObject buttonText;
    public Narrative narativeScript;

    public float speed;

    float timer;

    bool stepOneComplete;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f;

        narrativeText = gameObject.GetComponent<Text>();

        stepOneComplete = false;
    }
    void Update()
    {
        timer += Time.deltaTime;

        if (!stepOneComplete && narrativeText.color == midColor)
        {
            stepOneComplete = true;
        }
        else if (!stepOneComplete)
        {
            narrativeText.color = Color.Lerp(startColor, midColor, speed * timer);
        }

        if (buttonText.activeInHierarchy)
        {
            timer = 0.0f;
        }

        if (stepOneComplete && narativeScript.buttonPressed)
        {
            narrativeText.color = Color.Lerp(midColor, endColor, speed * timer);
        }
    }

    //public void FadeToBlack()
    //{
    //    //float t = (timer - startTime) * speed;
    //    narrativeText.color = Color.Lerp(startColor, midColor, speed * Time.deltaTime);
    //}
}