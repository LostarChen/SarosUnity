using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAddComponent : MonoBehaviour
{
    private SpriteRenderer empty; 
    // Start is called before the first frame update
    void Start()
    {
        empty = GameObject.Find("empty").AddComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
