using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMove : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    private float speed = 8f;
    public bool draw;
    public Rigidbody2D rb;
    public GameObject linePrefab;
    public Transform lineSpawnPoint;
    public Transform playerTransform;
    Line activeLine;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(horizontal * speed, vertical * speed);
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (Input.GetMouseButtonDown(1))
        {
            draw = true;
            GameObject newLine = Instantiate(linePrefab, lineSpawnPoint.position, lineSpawnPoint.rotation);
            activeLine = newLine.GetComponent<Line>();
        }
        if(activeLine!=null)
        {
            activeLine.UpdateLine(playerTransform.position);
        }
        if(!draw)
        {
            activeLine = null;
        }
        if (Input.GetMouseButtonDown(2))
        {
            draw = false;
        }
    }
}
