using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    private Color charColor = new Color((float)60/255, (float)167 /255, (float)250 /255, (float)255 /255);
    private SpriteRenderer charSprite;
    private void Awake()
    {
        charSprite = GameObject.Find("ship").GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            charSprite.color = charColor;
        }
    }
}
