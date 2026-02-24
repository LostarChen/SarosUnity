using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletShooting : MonoBehaviour
{
    WebSocketUnity wsUnity;
    public CharacterMove move;
    //private Transform bulletSpawnPoint;
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
   // public GameObject[] ship;
    public float bulletSpeed = 10;
    string playerName;
    string currentName;
    [Header("音效")]
    AudioSource audioSource;
    public AudioClip[] soundEffect;
    private void Awake()
    {
        wsUnity = GameObject.Find("WebSocketClient").GetComponent<WebSocketUnity>();
        audioSource = GameObject.Find("soundManager").GetComponent<AudioSource>();
        playerName = wsUnity.playerID;
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        currentName = wsUnity.playerID;
        if (wsUnity.bullet && playerName == currentName)
        {
            if(move.transform.localScale.x<0)
            {
                var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                bullet.GetComponent<Rigidbody2D>().velocity = bulletSpawnPoint.right * bulletSpeed*-1;
                bullet.GetComponent<Bullet>()._Renderer.color = move.charSprite.color;
                bullet.GetComponent<SpriteRenderer>().color = move.charSprite.color;
            }
            else if(move.transform.localScale.x > 0)
            {
                var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                bullet.GetComponent<Rigidbody2D>().velocity = bulletSpawnPoint.right * bulletSpeed;
                bullet.GetComponent<Bullet>()._Renderer.color = move.charSprite.color;
                bullet.GetComponent<SpriteRenderer>().color = move.charSprite.color;
            }
            audioSource.PlayOneShot(soundEffect[Random.Range(0,4)]);
            wsUnity.bullet = false;
        }

    }

}
