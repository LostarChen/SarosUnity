using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wandering : MonoBehaviour
{

    private float moveSpeed = 2f; // 调整移动速度
    public float maxDistanceFromCamera = 5f;

    private Camera mainCamera;
    private float cameraHeight;
    private float cameraWidth;
    public Rigidbody2D rb;
    public Transform npcTransform;
    private float timer = 0;
    public SpriteRenderer npcSprite;
    [Header("線條")]
    public GameObject linePrefab;
    private bool lineActivate;
    Line activeLine;
    void Start()
    {
        mainCamera = Camera.main;
        cameraHeight = 30;
        cameraWidth = 85;
        Debug.Log("cameraHeight:" + cameraHeight + " cameraWidth:" + cameraWidth);
    }

    void Update()
    {
        
        if(npcTransform.position.x>=cameraWidth)
        {
            rb.velocity = new Vector2(-5 * moveSpeed, Random.Range(-1, 2));
            timer = 3;
        }
        else if(npcTransform.position.x <= -cameraWidth)
        {
            rb.velocity = new Vector2(5 * moveSpeed, Random.Range(-1, 2));
            timer = 3;
        }
        else if(npcTransform.position.y >= cameraHeight)
        {
            rb.velocity = new Vector2(Random.Range(-1, 2), -5 * moveSpeed);
            timer = 3;
        }
        else if (npcTransform.position.y <= -cameraHeight)
        {
            rb.velocity = new Vector2(Random.Range(-1, 2), 5 * moveSpeed);
            timer = 3;
        }
        else
        {
            if (timer >= 0)
            {
                timer -= Time.deltaTime;
            }
            else if (timer < 0)
            {
                float randomX = Random.Range(-5, 5);
                float randomY = Random.Range(-5, 5);
                rb.velocity = new Vector2(randomX * moveSpeed, randomY * moveSpeed);
                timer = 3;
                activeLine = null;
                lineActivate = false;
            }
        }

        if (activeLine != null)
        {
            activeLine.UpdateLine(npcTransform.position);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("bullet"))
        {
            // 碰撞到标签为 "Obstacle" 的物体时执行的操作
            Debug.Log("bullet get");
            // 在这里添加你希望执行的操作，比如停止移动、播放音效等
            npcSprite.color = collision.gameObject.GetComponent<Bullet>()._Renderer.color;

            LineRenderer lineRenderer = linePrefab.GetComponent<LineRenderer>();
            lineRenderer.endColor = npcSprite.color;
            lineRenderer.startColor = npcSprite.color;
            GameObject newLine = Instantiate(linePrefab, npcTransform.position, npcTransform.rotation);
            activeLine = newLine.GetComponent<Line>();
            lineActivate = true;
        }
    }

}
