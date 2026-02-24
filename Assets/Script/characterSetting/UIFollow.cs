using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class UIFollow : MonoBehaviour
{
    WebSocketUnity wb;
    public Transform charTransform;
    public Canvas charUI;
    public Slider charHealth;
    public TMP_Text playerName;
    public SpriteRenderer playerSprite;
    public Sprite[] shipSprite;
    // Start is called before the first frame update
    private void Awake()
    {
        wb = GameObject.Find("WebSocketClient").GetComponent<WebSocketUnity>();
    }
    void Start()
    {
        if (wb.playerID != null)
        {
            playerName.text = wb.playerID;
            //charHealth.maxValue = wb.health;
        }
        switch (wb.playerImg)
        {
            case "Bishop.png":
                playerSprite.sprite = shipSprite[0];
                break;
            case "Explorer.png":
                playerSprite.sprite = shipSprite[1];
                break;
            case "Satellite.png":
                playerSprite.sprite = shipSprite[2];
                break;
            case "Traveler.png":
                playerSprite.sprite = shipSprite[3];
                break;
            case "Triangle.png":
                playerSprite.sprite = shipSprite[4];
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        charUI.transform.position = charTransform.position + new Vector3(0, 6f, 0);
    }
}
