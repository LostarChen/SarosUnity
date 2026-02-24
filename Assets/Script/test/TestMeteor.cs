using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMeteor : MonoBehaviour
{
    public GameObject test;
    public GameObject meteor;
    public GameObject boom;
    // Start is called before the first frame update
    void Start()
    {
        
        Transform spawnPoint = test.GetComponent<Transform>();
        Instantiate(meteor, spawnPoint);
        Instantiate(boom, spawnPoint);
    }
}
