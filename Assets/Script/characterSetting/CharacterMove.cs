using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMove : MonoBehaviour
{
    WebSocketUnity wsUnity;
    private float speed;
    public float horizontal;
    public float verticle;
    public bool facingRight = true;
    public Rigidbody2D rb;
    public SpriteRenderer charSprite;
    private Sprite originSprite;
    private Color originColor;
    public Transform playerPosition;
    string playerName;
    string currentName;
    float colorChangeTime =5;
    private float cameraWidth;
    private float cameraHeight;
    private float change = 0;
    float transportTime =0;
    public Slider playerSlider;
    public bool playerDead = false;
    private float playerDeadTimer = 3;
    [Header("聲音")]
    private AudioSource audioSource;
    public AudioClip transportSound;
    // Start is called before the first frame update
    private void Awake()
    {
        audioSource = GameObject.Find("soundManager").GetComponent<AudioSource>();
        wsUnity = GameObject.Find("WebSocketClient").GetComponent<WebSocketUnity>();
        playerName = wsUnity.playerID;
        speed = 7;
        cameraHeight = 30;
        cameraWidth = 85;
    }
    void Start()
    {
        originColor = new Color(wsUnity.r, wsUnity.g, wsUnity.b, 1.0f);
        charSprite.color = originColor;
        originSprite = charSprite.sprite;
        playerSlider.maxValue = 200;
        playerSlider.value = 200;
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {  
        Flip();
    }
    private void Update()
    {
        if(playerPosition.position.x>cameraWidth)
        {
            horizontal = -1;
        }
        else if(playerPosition.position.x < -cameraWidth)
        {
            horizontal = 1;
        }
        else if (playerPosition.position.y > cameraHeight)
        {
            verticle = -1;
        }
        else if (playerPosition.position.y < -cameraHeight)
        {
            verticle = 1;
        }
        rb.velocity = new Vector2(horizontal*speed, verticle*speed);
        //horizontal = Input.GetAxisRaw("Horizontal");
        //verticle = Input.GetAxisRaw("Vertical");
        currentName = wsUnity.playerID;
        if(wsUnity.charMove!=null&&playerName == currentName&&!playerDead)
        {
            switch(wsUnity.charMove)
            {
                case "up":
                    verticle=1;
                    horizontal = 0;
                    break;
                case "down":
                    verticle=-1;
                    horizontal = 0;
                    break;
                case "origin":
                    verticle=0;
                    horizontal = 0;
                    rb.angularVelocity = 0;
                    playerPosition.rotation = new Quaternion(0, 0, 0, 0);
                    break;
                case "right":
                    verticle = 0;
                    horizontal =1;
                    break;
                case "left":
                    verticle = 0;
                    horizontal =-1;
                    break;
            }
        }
        ColorChange();
        PlayerDead();
        if (transportTime>0)
        {
            transportTime -= Time.deltaTime;
        }
    
    }
    private void Flip()
    {
        if(facingRight&&horizontal<0f||!facingRight&& horizontal > 0f)
        {
            facingRight = !facingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*
        if (collision.gameObject.CompareTag("bullet"))
        {
            // 碰撞到标签为 "Obstacle" 的物体时执行的操作
            Debug.Log("bullet get");
            // 在这里添加你希望执行的操作，比如停止移动、播放音效等
            charSprite.color = collision.gameObject.GetComponent<Bullet>().bulletColor;
            playerSlider.value -= 20;
        }
        
        if (collision.gameObject.CompareTag("blackHole")&&transportTime<=0)
        {
            Transform transfer = collision.gameObject.GetComponent<blackHole>()._Rt;
            playerPosition.position = transfer.position;
            audioSource.PlayOneShot(transportSound);
            transportTime = 5;
        }*/
        if (collision.gameObject.CompareTag("alien"))
        {
            charSprite.sprite = collision.gameObject.GetComponent<SpriteRenderer>().sprite;
            change = 5;
            StartCoroutine(UpdateCharSprite());
        }
    }
    void ColorChange()
    {
        if (charSprite.color != originColor&&!playerDead)
        {

            if (colorChangeTime < 0)
            {
                charSprite.color = originColor;
                colorChangeTime = 5;
            }
            else
            {
                colorChangeTime -= Time.deltaTime;
            }
        }
    }
    IEnumerator UpdateCharSprite()
    {
        while(change>0)
        {
            change -= 1;
            yield return new WaitForSeconds(1);
        }
        charSprite.sprite = originSprite;
    }
    void PlayerDead()
    {
        if(playerSlider.value<=0)
        {
            if(playerDeadTimer>0)
            {
                playerDeadTimer -= Time.deltaTime;
                charSprite.color = new Color(originColor.r, originColor.g, originColor.b, 0);
                playerDead = true;
            }
            else if(playerDeadTimer<=0)
            {
                charSprite.color = new Color(originColor.r, originColor.g, originColor.b, 1);
                playerSlider.value = playerSlider.maxValue;
                playerDead = false;
                playerDeadTimer = 3;
            }
        }


    }
}
