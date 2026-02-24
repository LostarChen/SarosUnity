using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Transform _Rt;
    public Transform _CharacterRt;
    [Header("Script")]
    public ShootController _ShootController;
    public DrawLine _DrawLine;
    public Stamping _Stamping;
    public MoveCharacter _MoveCharacter;
    public CharacterUI _CharacterUI;
    [Header("PlayerInfo")]
    [SerializeField] private Player _Player;
    [Header("BtnData")]
    public bool _IsTeleportLocked = false;
    [Header("Outfit")]
    [SerializeField] private SpriteRenderer _SpriteRenderer;
    private void OnEnable()
    {
        _MoveCharacter._Rt = _Rt;
        _MoveCharacter._CharacterRT = _CharacterRt;
    }
    private void Update()
    {
        _MoveCharacter.CollideBorder(_Player);
    }
    public string GetPlayerId()
    {
        return _Player._PlayerId;
    }
    public void InitPlayerData(Player player,Sprite sprite,Sprite stampSprite)
    {
        _Player = player;
        _CharacterUI.InitPlayerUI(player._PlayerName);
        _SpriteRenderer.sprite = sprite;
        _SpriteRenderer.color = player._Color;
        _Stamping._StampSprite = stampSprite;
    }
    public void UpdatePlayer(Player player)
    {
        if(player._Direction!=_Player._Direction)
        {
            _MoveCharacter.Move(player._Direction);
        }
        if(player._IsShooting!=_Player._IsShooting)
        {
            _ShootController.ShootBullet(player, _CharacterRt);
        }
        if(player._IsDrawing!=_Player._IsDrawing)
        {
            _DrawLine.SetDraw(player._IsDrawing,player._Color);
        }
        if(player._IsStamping!=_Player._IsStamping)
        {
            _Stamping.MakeStamp(player._Color);
        }
        _Player = player;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Bullet bullet))
        {
            _CharacterUI.UpdateHealth(-10);
            Color bulletColor = bullet._Renderer.color;
            _SpriteRenderer.color = bulletColor;
            StartCoroutine(ReturnBackColor());
        }
    }
    private IEnumerator ReturnBackColor()
    {
        yield return new WaitForSeconds(3);
        _SpriteRenderer.color = _Player._Color;
    }
}
