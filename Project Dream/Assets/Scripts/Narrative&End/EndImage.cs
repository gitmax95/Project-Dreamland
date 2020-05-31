using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndImage : MonoBehaviour
{
    Image backgroundImage;

    public Color startColor;
    public Color endColor;
    //public Color textColorFinal;

    //public GameObject buttonText;
    public MainMenu sceneManager;

    public float speed;
    //public bool buttonPressed;
    public EndText1 endText1;
    public EndText2 endText2;

    float timer;

   // bool stepOneComplete;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        timer = 0.0f;

        backgroundImage = gameObject.GetComponent<Image>();

        //stepOneComplete = false;
        //buttonPressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        

        //if (!stepOneComplete && backgroundImage.color == endColor)
        //{
        //    stepOneComplete = true;
        //    //buttonText.SetActive(true);
        //}
        if (endText1.GetTextColor() == endText1.endColor && endText2.GetTextColor() == endText2.endColor)
        {
            timer += Time.deltaTime;
            backgroundImage.color = Color.Lerp(startColor, endColor, speed * timer);
        }

        //if (stepOneComplete /*&& buttonPressed*/)
        //{
        //    backgroundImage.color = Color.Lerp(midColor, endColor, speed * timer);/*FadeToBlack();*/
        //}

        if (backgroundImage.color == endColor)
        {
            sceneManager.MainMenuScene();
        }

        //print(Time.timeScale);
    }
}
