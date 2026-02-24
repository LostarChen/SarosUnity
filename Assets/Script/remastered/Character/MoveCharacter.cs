using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCharacter : MonoBehaviour
{
    public Transform _Rt;
    public Transform _CharacterRT;
    public Rigidbody2D _Rb;
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float horizontal;
    [SerializeField] private float verticle;
    private float cameraWidth;
    private float cameraHeight;

    private void Start()
    {
        cameraHeight = 30;
        cameraWidth = 85;
    }
    void Update()
    {
        _Rb.velocity = new Vector2(horizontal * speed, verticle * speed);
    }
    public void Move(string direction)
    {
        switch (direction)
        {
            case "up":
                verticle = 1;
                horizontal = 0;
                Debug.Log($"[MoveCharacter] Move() up");
                break;
            case "down":
                verticle = -1;
                horizontal = 0;
                Debug.Log($"[MoveCharacter] Move() down");
                break;
            case "origin":
                verticle = 0;
                horizontal = 0;
                _Rb.angularVelocity = 0;
                _Rt.rotation = new Quaternion(0, 0, 0, 0);
                break;
            case "right":
                verticle = 0;
                horizontal = 1;
                if (_CharacterRT.localScale.x < 0) ChangeFaceDirection();
                Debug.Log($"[MoveCharacter] Move() right");
                break;
            case "left":
                if (_CharacterRT.localScale.x > 0) ChangeFaceDirection();
                verticle = 0;
                horizontal = -1;
                Debug.Log($"[MoveCharacter] Move() left");
                break;

        }
    }
    public void CollideBorder(Player player)
    {
        if (_Rt.position.x > cameraWidth)
        {
            if (_CharacterRT.localScale.x > 0) ChangeFaceDirection();
            player._Direction = "left";
            horizontal = -1;
        }
        else if (_Rt.position.x < -cameraWidth)
        {
            player._Direction = "right";
            if (_CharacterRT.localScale.x < 0) ChangeFaceDirection();
            horizontal = 1;
        }
        else if (_Rt.position.y > cameraHeight)
        {
            player._Direction = "down";
            verticle = -1;
        }
        else if (_Rt.position.y < -cameraHeight)
        {
            player._Direction = "up";
            verticle = 1;
        }
    }
    private void ChangeFaceDirection()
    {
        _CharacterRT.localScale = new Vector3(-_CharacterRT.localScale.x, _CharacterRT.localScale.y);
    }
}
