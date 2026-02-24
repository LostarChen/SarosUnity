using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meteor : MonoBehaviour
{
    public GameObject meteorPrefab;
    public GameObject boomPrefab;
    public GameObject stonePrefab;
    GameObject[] spawnPoint = new GameObject[4];
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MeteorCreate());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator MeteorCreate()
    {
        while(true)
        {
            for (int i = 0; i < 4; i++)
            {
                spawnPoint[i] = new GameObject("meteorSpawnPoint_" + i);
                spawnPoint[i].transform.position = new Vector3(Random.Range(80, -80), Random.Range(50, -10), 0);
                GameObject meteor = Instantiate(meteorPrefab, spawnPoint[i].transform);
                GameObject boom = Instantiate(boomPrefab, spawnPoint[i].transform);
            }
            foreach (GameObject obj in spawnPoint)
            {
                if (obj != null)
                {
                    Transform meteorTransform = obj.transform.Find("meteor(Clone)");
                    Transform boomTransform = obj.transform.Find("boom(Clone)");
                    if (meteorTransform != null)
                    {
                        Destroy(meteorTransform.gameObject, 2);
                    }
                    if (boomTransform != null)
                    {
                        Destroy(boomTransform.gameObject, 2);
                    }

                }
            }
            yield return new WaitForSeconds(2);
            for(int j =0;j<4; j++)
            {
                GameObject stone = Instantiate(stonePrefab, spawnPoint[j].transform);
            }
            foreach (GameObject obj in spawnPoint)
            {
                if (obj != null)
                {
                    Destroy(obj, 10);
                }
            }
            yield return new WaitForSeconds(20);
        }
    }
}
