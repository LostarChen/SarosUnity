using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StampGenerate : MonoBehaviour
{
    WebSocketUnity wsUnity;
    public GameObject[] ship;
    public GameObject stampPrefab;
    public GameObject shipGB;
    private SpriteRenderer stampColor;
    private Color originColor;
    public Transform stampTransform;
    string playerName;
    string currentName;
    // Start is called before the first frame update
    private void Awake()
    {
        wsUnity = GameObject.Find("WebSocketClient").GetComponent<WebSocketUnity>();
        //stampColor = ship[0].AddComponent<SpriteRenderer>();
        playerName = wsUnity.playerID;
        switch (wsUnity.playerImg)
        {
            case "Bishop.png":
                stampPrefab = ship[0];
                break;
            case "Explorer.png":
                stampPrefab = ship[1];
                break;
            case "Satellite.png":
                stampPrefab = ship[2];
                break;
            case "Traveler.png":
                stampPrefab = ship[3];
                break;
            case "Triangle.png":
                stampPrefab = ship[4];
                break;
        }
    }
    void Start()
    {
         stampColor = stampPrefab.GetComponent<SpriteRenderer>();
         originColor = new Color(wsUnity.r, wsUnity.g, wsUnity.b, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        currentName = wsUnity.playerID;
        if(wsUnity.stamp&&playerName == currentName)
        {
            stampColor.color = originColor;
            GameObject stamping = Instantiate(stampPrefab, stampTransform.position, stampTransform.rotation);
            wsUnity.stamp = false;
        }
    }

}
