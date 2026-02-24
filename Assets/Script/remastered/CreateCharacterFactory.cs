using System.Collections;
using System.Collections.Generic;
using Firebase.Database;
using UnityEngine;

public class CreateCharacterFactory : MonoBehaviour
{
    public Character _Character;
    [SerializeField]private List<CharacterSprite> _CharacterSprites = new List<CharacterSprite>();
    public Character SpawnCharacter(Player player)
    {
        Vector3 spawnPos = new Vector3(
        Random.Range(-50f, 50f),
        Random.Range(-20f, 20f),
        0f);
        
        var character = Instantiate(_Character, spawnPos,Quaternion.identity);
        var characterSprite = GetSprite(player._SpriteName);
        character.InitPlayerData(player, characterSprite._Sprite, characterSprite._Stamp);
        return character;
    }
    private CharacterSprite GetSprite(string spriteName)
    {
        foreach(var charSprite in _CharacterSprites)
        {
            if(spriteName == charSprite._SpriteName)
            {
                return charSprite;
            }
        }
        return _CharacterSprites[0];
    }


}
[System.Serializable]
public class Player
{
    public string _PlayerId;
    public string _PlayerName;
    public string _Direction;
    public string _SpriteName;
    public Color _Color;
    public bool _IsShooting;
    public bool _IsStamping;
    public bool _IsDrawing;


    public Player(string id, string name,string spriteName,Color color)
    {
        _PlayerId = id;
        _PlayerName = name;
        _Direction = "";
        _SpriteName = spriteName;
        _Color = color;
        _IsShooting = false;
        _IsStamping = false;
        _IsDrawing = false;

    }
}
[System.Serializable]
public class CharacterSprite
{
    public string _SpriteName;
    public Sprite _Sprite;
    public Sprite _Stamp;
}

