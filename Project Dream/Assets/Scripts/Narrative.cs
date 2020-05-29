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
    
    float startTime;

    bool stepOneComplete = false;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        backgroundImage = gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (backgroundImage.color == midColor)
        {
            stepOneComplete = true;
            startTime = Time.time;
            buttonText.SetActive(true);
        }
        else if (backgroundImage.color != midColor && buttonText.activeInHierarchy)
        {
            buttonText.SetActive(false);
        }

        if (!stepOneComplete)
        {
            float t = (Time.time - startTime) * speed;
            backgroundImage.color = Color.Lerp(startColor, midColor, t);
        }
        if (stepOneComplete)
        {
            FadeToBlack();
        }
        if (backgroundImage.color == endColor)
        {
            sceneManager.BridgeScene();
        }
}
    public void FadeToBlack()
    {
        float t = (Time.time - startTime) * speed;
        backgroundImage.color = Color.Lerp(midColor, endColor, t);
    }
}
