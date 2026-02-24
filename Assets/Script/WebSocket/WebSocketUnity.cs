using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using Newtonsoft.Json;
using System.IO;

public class WebSocketUnity : MonoBehaviour
{
    private WebSocket ws;
    public List<string> existingNames = new List<string>();
    private float onMessageTimer;
    public float delayTime;
    public string playerID;
    public int health;
    public string playerImg;
    public string[] playerPart;
    public GameObject charPrefab;
    private GameObject ship;
    public Transform charSpawnPoint;
    private bool charSpawn;
    private bool newID = false;
    [Header("傳遞變數")]
    public string charMove;
    public bool bullet;
    public bool stamp;
    public bool draw;
    public float r;
    public float g;
    public float b;

    public class PlayerData
    {
        public string playerName;
        public string charImg;
        public string direction;
        public string bullet;
        public string stamp;
        public string draw;
        public float r;
        public float g;
        public float b;
    }
    private void Awake()
    {

    }
    void Start()
    {
        ws = new WebSocket("wss://saros.newmedia.tw:4443"); // 设置您的服务器IP地址和端口
        ws.OnMessage += OnMessage;

        ws.Connect();

        //ws.Send("Hello from unity3D haha"); 

        StartCoroutine(WSUpdate());
    }

    private void Update()
    {
        /*
        if (ws != null && ws.IsAlive)
        {
            // Process incoming messages while the connection is open
            ws.Ping(); // Keep the connection alive
        }
        if (charSpawn)
        {
            Instantiate(charPrefab, charSpawnPoint.position+new Vector3(Random.Range(10,-10), Random.Range(10, -10),0), Quaternion.identity);
            charSpawn = false;
            newID = false;
        }
        */
    }

    private IEnumerator WSUpdate()
    {
        int cd = 50;
        while (true)
        {
            if (ws != null && ws.IsAlive)
            {
                // Process incoming messages while the connection is open
                cd--;
                if (cd < 0)
                {
                    ws.Ping(); // Keep the connection alive
                    cd = 50;
                }
            }
            if (charSpawn)
            {
                Instantiate(charPrefab, charSpawnPoint.position + new Vector3(Random.Range(10, -10), Random.Range(10, -10), 0), Quaternion.identity);
                charSpawn = false;
                newID = false;
            }
            yield return new WaitForSeconds(0.01f);
        }
    }

    private void OnMessage(object sender, MessageEventArgs e)
    {
        string receivedMessage = e.Data;
        Debug.Log("Received message: " + receivedMessage);
        // 处理从服务器接收的消息，例如更新游戏状态
        if (e.Data != null)
        {
            Debug.Log(e.Data);
            PlayerData playerData = JsonConvert.DeserializeObject<PlayerData>(receivedMessage);
            if (playerData != null)
            {
                Debug.Log($"Player Name: {playerData.playerName},CharImg: {playerData.charImg},Direction:{playerData.direction},Bullet:{playerData.bullet}"
                    + $"Stamp:{playerData.stamp},Draw:{playerData.draw},r:{playerData.r},g:{playerData.g},b:{playerData.b}");
                if (playerData.playerName is string)
                {
                    playerID = playerData.playerName;
                }
                else
                {
                    playerID = (string)playerData.playerName;
                }
                if (existingNames.Contains(playerID))
                {
                    Debug.Log("Name already exists!");
                }
                else
                {
                    existingNames.Add(playerID);
                    newID = true;
                }
                playerImg = Path.GetFileName(playerData.charImg);
                Debug.Log(playerImg);
                charMove = playerData.direction;
                r = playerData.r / 255;
                g = playerData.g / 255;
                b = playerData.b / 255;
                if (playerData.draw != null)
                {
                    draw = true;
                }
                if (playerData.bullet != null)
                {
                    bullet = true;
                }
                if (playerData.stamp != null)
                {
                    stamp = true;
                }
                if (playerData.charImg != null && newID)
                {
                    charSpawn = true;
                }
            }
            else
            {
                Debug.LogError("Failed to deserialize JSON data");
            }
        }
        else
        {
            Debug.LogError("JSON file does not exist");
        }

    }
    public void SendMessageToServer(string message)
    {
        if (ws != null && ws.ReadyState == WebSocketState.Open)
        {
            ws.Send(message); // 发送消息到服务器
        }
    }

    void OnDestroy()
    {
        if (ws != null && ws.ReadyState == WebSocketState.Open)
        {
            ws.Close();
        }
    }

}
