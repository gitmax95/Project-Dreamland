using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndText1 : MonoBehaviour
{
    Text text1;

    public Color startColor;
    public Color midColor;
    public Color endColor;

    public EndText2 endText2;
    public EndImage endImage;

    public float speed;

    float timer;

    bool stepOneComplete;
    bool resetTimer;
    bool text2SOC;

    void Start()
    {
        timer = 0.0f;

        text1 = gameObject.GetComponent<Text>();

        stepOneComplete = false;
        resetTimer = false;
        text2SOC = false;
    }

    // Update is called once per frame
    void Update()
    {
        GetTextColor();

        timer += Time.deltaTime;

        if (!stepOneComplete && text1.color == midColor)
        {
            stepOneComplete = true;
        }
        else if (!stepOneComplete)
        {
            text1.color = Color.Lerp(startColor, midColor, speed * timer);
        }

        if (endText2.GetTextColor() == midColor && !text2SOC)
        {
            text2SOC = true;
        }

        if (stepOneComplete && text2SOC)
        {
            if (!resetTimer)
            {
                timer = 0.0f;
                resetTimer = true;
            }
            text1.color = Color.Lerp(midColor, endColor, speed * timer);
        }
    }
    public Color GetTextColor()
    {
        return text1.color;
    }
}
