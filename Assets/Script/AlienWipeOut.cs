using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienWipeOut : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("bullet"))
        {
            Destroy(collision.gameObject);
            Debug.Log("wipe");
        }
    }
}
