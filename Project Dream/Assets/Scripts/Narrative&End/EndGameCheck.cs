using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameCheck : MonoBehaviour
{
    public GameObject fadeObject;

    public MainMenu sceneManager;

    public Color startColor;
    public Color endColor;

    public float speed;

    float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (gameObject.GetComponent<BoxCollider2D>().enabled == false)
        {
            if (!fadeObject.activeInHierarchy)
            {
                fadeObject.SetActive(true);
                timer = 0.0f;
            }
            fadeObject.GetComponent<Image>().color = Color.Lerp(startColor, endColor, speed * timer);
        }

        if (fadeObject.GetComponent<Image>().color == endColor)
        {
            sceneManager.EndScene();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            print("I collide");
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
