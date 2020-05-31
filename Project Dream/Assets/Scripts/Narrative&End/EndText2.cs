using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndText2 : MonoBehaviour
{
    Text text2;

    public Color startColor;
    public Color midColor;
    public Color endColor;

    public EndText1 endText1;
    public EndImage endImage;

    public float speed;

    float timer;

    bool stepOneComplete;
    bool resetTimer1;
    bool resetTimer2;

    void Start()
    {
        timer = 0.0f;

        text2 = gameObject.GetComponent<Text>();

        stepOneComplete = false;
        resetTimer1 = false;
        resetTimer2 = false;
    }

    // Update is called once per frame
    void Update()
    {
        GetTextColor();

        timer += Time.deltaTime;

        if (!stepOneComplete && text2.color == midColor)
        {
            stepOneComplete = true;
        }
        else if (!stepOneComplete && endText1.GetTextColor() == midColor)
        {
            if (!resetTimer1)
            {
                timer = 0.0f;
                resetTimer1 = true;
            }
            text2.color = Color.Lerp(startColor, midColor, speed * timer);
        }

        if (stepOneComplete)
        {
            if (!resetTimer2)
            {
                timer = 0.0f;
                resetTimer2 = true;
            }
            text2.color = Color.Lerp(midColor, endColor, speed * timer);
        }
    }
    public Color GetTextColor()
    {
        return text2.color;
    }
}