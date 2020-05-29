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

    public float speed;

    float startTime;

    bool stepOneComplete = false;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        narrativeText = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (narrativeText.color == midColor)
        {
            stepOneComplete = true;
            startTime = Time.time;
        }

        if (!stepOneComplete)
        {
            float t = (Time.time - startTime) * speed;
            narrativeText.color = Color.Lerp(startColor, midColor, t);
        }
        if (stepOneComplete)
        {
            FadeToBlack();
        }
    }

    public void FadeToBlack()
    {
        float t = (Time.time - startTime) * speed;
        narrativeText.color = Color.Lerp(midColor, endColor, t);
    }
}