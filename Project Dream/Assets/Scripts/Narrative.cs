using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Narrative : MonoBehaviour
{
    Image backgroundImage;

    public Color startColor;
    public Color midColor;
    public Color endColor;

    public GameObject buttonText;
    public MainMenu sceneManager;

    public float speed;
    public bool buttonPressed;

    float timer;

    bool stepOneComplete;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        timer = 0.0f;

        backgroundImage = gameObject.GetComponent<Image>();

        stepOneComplete = false;
        buttonPressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (!stepOneComplete && backgroundImage.color == midColor)
        {
            stepOneComplete = true;
            buttonText.SetActive(true);
        }
        else if (!stepOneComplete)
        {
            backgroundImage.color = Color.Lerp(startColor, midColor, speed * timer);
        }

        if (stepOneComplete && buttonPressed)
        {
            backgroundImage.color = Color.Lerp(midColor, endColor, speed * timer);/*FadeToBlack();*/
        }

        if (backgroundImage.color == endColor)
        {
            sceneManager.BridgeScene();
        }

        //print(Time.timeScale);
}
    //public void FadeToBlack()
    //{
    //    backgroundImage.color = Color.Lerp(midColor, endColor, speed * timer/* Mathf.MoveTowards(0.0f, 1.0f, speed) * Time.deltaTime*/);
    //}
    public void PressButton()
    {
        if (buttonText.activeInHierarchy)
        {
            buttonText.SetActive(false);
            buttonPressed = true;
            timer = 0.0f;
        }
    }
}
