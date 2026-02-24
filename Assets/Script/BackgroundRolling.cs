using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundRolling : MonoBehaviour
{
    [Range(-1f,1f)]
    public float rollingSpeed;
    private float rollDelta = 0;
    [SerializeField]private Material bgMaterial;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Rolling();
    }
    private void Rolling()
    {
        rollDelta += (Time.deltaTime * rollingSpeed) / 10f;
        bgMaterial.mainTextureOffset = new Vector2(rollDelta,0);

    }

}
