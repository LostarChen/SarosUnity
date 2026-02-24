using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamping : MonoBehaviour
{
    public Sprite _StampSprite; 
    public Stamp _Stamp;
    public Transform _StampTransform;
    public void MakeStamp(Color color)
    {
        var stamping = Instantiate(_Stamp, _StampTransform.position, _StampTransform.rotation);
        stamping._Renderer.sprite = _StampSprite;
        stamping._Renderer.color = color;
    }
}
