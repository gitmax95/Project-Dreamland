using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LucidState : MonoBehaviour
{
    GameObject player;
    GameObject ComponentUI;

    Sprite lucidIconW;
    Sprite lucidIconB;

    public bool isLucid;
    public float lucidTime;
    public int lucidCharges;

    public float lucidTimer = 0.0f;

    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlayerChar");
        ComponentUI = GameObject.Find("LucidIcon");

        isLucid = false;

        lucidIconW = Resources.Load<Sprite>("Sprites/WhiteSmall");
        lucidIconB = Resources.Load<Sprite>("Sprites/BlackSmall");
    }

    // Update is called once per frame
    void Update()
    {
        MoveToPlayer();
        LucidStateToggle();
        SpriteState();
    }

    private void LucidStateToggle()
    {
        if (Input.GetKey(KeyCode.L) && lucidTimer == 0.0f && lucidCharges != 0)
        {
            isLucid = true;
            lucidCharges--;
        }
        else if (lucidTimer > lucidTime)
        {
            isLucid = false;
        }
    }

    private void MoveToPlayer() //temporary till we get UI
    {
        ComponentUI.transform.position = new Vector3(player.transform.position.x + 0.5f, player.transform.position.y + 0.1f, player.transform.position.z - 2.0f);
    }

    private void SpriteState()
    {
        if (isLucid)
        {
            lucidTimer += Time.deltaTime;
            ComponentUI.GetComponent<SpriteRenderer>().sprite = lucidIconW;
        }
        else if (!isLucid)
        {
            lucidTimer = 0.0f;
            ComponentUI.GetComponent<SpriteRenderer>().sprite = lucidIconB;
        }
    }
   // private void SpriteState()
}
