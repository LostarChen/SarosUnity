using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetHealth : MonoBehaviour
{
    public Slider planetHealth;
    public SpriteRenderer planet;
    public SpriteRenderer planetBall;
    public Sprite[] planetModel;
    public Bullet planetBulletPrefab;
    public Transform planetTransform;
    public Animator alienAnimator;
    [SerializeField]private Animator planetAnimator;
    public Animator transitionAnimtor;
    GameObject[] spawnPoint = new GameObject[8];
    int spawnPointNumber = 8;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < spawnPointNumber; i++)
        {
            float angle = i * (360f / spawnPointNumber); // 计算角度
            float x = 20 * Mathf.Cos(angle * Mathf.Deg2Rad);
            float y = 20 * Mathf.Sin(angle * Mathf.Deg2Rad);
            // 计算圆周上的点的坐标
            GameObject newSpawnPoint = new GameObject("SpawnPoint_" + i);
            newSpawnPoint.transform.position = new Vector3(x, y, 0) + planetTransform.position;

            // 存储在数组中
            spawnPoint[i] = newSpawnPoint;
            //planetAnimator = GameObject.Find("planetSprite").GetComponent<Animator>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(planetHealth.value<=planetHealth.maxValue*0.9&&planetHealth.value>planetHealth.maxValue*0.7)
        {
           // planet.sprite = planetModel[1];
        }
        else if (planetHealth.value <= planetHealth.maxValue * 0.7 && planetHealth.value > planetHealth.maxValue * 0.5)
        {
            //planet.sprite = planetModel[2];
            alienAnimator.SetBool("alien", true);
        }
        else if (planetHealth.value <= planetHealth.maxValue * 0.5 && planetHealth.value > planetHealth.maxValue * 0.3)
        {
            // planet.sprite = planetModel[3];
            planetAnimator.SetBool("halfHealth", true);
        }
        else if (planetHealth.value <= planetHealth.maxValue * 0.3 && planetHealth.value > planetHealth.maxValue * 0.1)
        {
           // planet.sprite = planetModel[4];
        }
        else if (planetHealth.value <= planetHealth.maxValue * 0.1 )
        {
            planetAnimator.SetTrigger("dead");
            //s planet.sprite = planetModel[5];
            transitionAnimtor.SetTrigger("transit");
            planetAnimator.SetBool("halfHealth", false);
            planetHealth.maxValue+=100;
            planetHealth.value = planetHealth.maxValue;
            GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Line");

            // 遍歷並銷毀每個找到的物件
            foreach (GameObject obj in objectsWithTag)
            {
                Destroy(obj);
            }
        }

    }
    void PlanetBulletSpawnPoint(int _spawnPointNumber)
    {
        Transform[] planetBulletSpawn = new Transform[_spawnPointNumber];

        for (int i = 0; i < _spawnPointNumber; i++)
        {
            float angle = i * (360f / spawnPointNumber); // 计算角度

            // 计算圆周上的点的坐标
            float x = 20 * Mathf.Cos(angle * Mathf.Deg2Rad);
            float y = 20 * Mathf.Sin(angle * Mathf.Deg2Rad);

            // 创建一个空 GameObject 作为 spawn point，并设置其位置


            // 将这个空 GameObject 的 transform 存入数组
            planetBulletSpawn[i] = spawnPoint[i].transform;

            // 在 spawn point 位置实例化子弹
            var planetBullet = Instantiate(planetBulletPrefab, spawnPoint[i].transform.position, Quaternion.identity);
            planetBullet._Rb.velocity = planetBulletSpawn[i].right * x + planetBulletSpawn[i].up * y;
            planetBullet._Renderer.color = planet.color;

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Bullet bullet))
        {
            planetHealth.value -= 100;
            Color bulletColor = bullet._Renderer.color;
            planet.color = bulletColor;
            planetBall.color = planet.color;
            PlanetBulletSpawnPoint(8);
        }

    }

}
