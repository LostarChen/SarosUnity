using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StampDisappear : MonoBehaviour
{
    public GameObject stamp;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StampDisappearing());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator StampDisappearing()
    {
        float disappear = 1;
        SpriteRenderer _stampColor = stamp.GetComponent<SpriteRenderer>();
        while (disappear >= 0)
        {
            disappear -= 0.01f;
            yield return new WaitForSeconds(0.1f);
            _stampColor.color = new Color(_stampColor.color.r, _stampColor.color.g, _stampColor.color.b, disappear);
        }
        Destroy(stamp);
    }
}
