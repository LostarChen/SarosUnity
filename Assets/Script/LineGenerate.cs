using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineGenerate : MonoBehaviour
{
    WebSocketUnity wsUnity;
    public GameObject linePrefab;
    public Transform lineSpawnPoint;
    public Transform playerTransform;
    string currentName;
    bool lineActivate;
    public bool draw;
    public Color currentColor;
    Line activeLine;
    // Start is called before the first frame update
    private void Awake()
    {
        wsUnity = GameObject.Find("WebSocketClient").GetComponent<WebSocketUnity>();
        currentName = wsUnity.playerID;
        currentColor = new Color(wsUnity.r, wsUnity.g, wsUnity.b);
    }
    private void Start()
    {
        
    }
    void Update()
    {
        if(wsUnity.draw && wsUnity.playerID == currentName)
        {
            if(draw)
            {
                draw = false;
                Debug.Log("no drawing");
            }
            else if(!draw)
            {
                draw = true;
                Debug.Log("drawing");
            }
            wsUnity.draw = false;
        }
        if (!lineActivate)
        {
            if (draw)
            {
                LineRenderer lineRenderer = linePrefab.GetComponent<LineRenderer>();
                //linePrefab.GetComponent<Line>().lineEndColor = currentColor;
                lineRenderer.endColor = currentColor;
                lineRenderer.startColor = new Color(0,0,0,0);
                GameObject newLine = Instantiate(linePrefab, lineSpawnPoint.position, lineSpawnPoint.rotation);
                activeLine = newLine.GetComponent<Line>();
                lineActivate = true;
            }
        }
        if(!draw)
        {
            activeLine = null;
            lineActivate = false;
        }
        if (activeLine != null)
        {
            activeLine.UpdateLine(playerTransform.position);
        }
    }
}
