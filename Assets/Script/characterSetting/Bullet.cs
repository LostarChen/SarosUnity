using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class Bullet : MonoBehaviour
{
    public Rigidbody2D _Rb;
    public float _Life = 3;
    public SpriteRenderer _Renderer;
    private void Awake()
    {
        Destroy(this.gameObject, _Life);
    }
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
        Debug.Log("onCollision");
    }

}
