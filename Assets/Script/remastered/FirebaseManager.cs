using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine;

public class FirebaseManager : MonoBehaviour
{
    private DatabaseReference _DbReference;
    public CreateCharacterFactory _CreateCharFactory;
    public Dictionary<string, Character> _CharacterDic = new Dictionary<string, Character>();

    private void Awake()
    {
        FirebaseApp.CheckAndFixDependenciesAsync()
                   .ContinueWithOnMainThread(task =>
                   {
                       if (task.Result == DependencyStatus.Available)
                       {
                           InitializeFirebase();
                           StartListening();
                       }
                       else
                       {
                           Debug.LogError("Firebase not available: " + task.Result);
                       }
                   });
    }
    void Start()
    {
        
    }

    void InitializeFirebase()
    {
        _DbReference = FirebaseDatabase.DefaultInstance.RootReference;
        Debug.Log("Firebase Initialized");
    }
    private void StartListening()
    {
        var userRef = _DbReference.Child("User");
        userRef.ChildAdded += OnPlayerAdded;
        userRef.ChildChanged += OnPlayerChanged;

        userRef.OnDisconnect().RemoveValue();
    }
    private void OnPlayerAdded(object sender, ChildChangedEventArgs args)
    {
        string playerId = args.Snapshot.Key;

        Player player = ParsePlayer(args.Snapshot);
        if (!_CharacterDic.ContainsKey(playerId))
        {
            var character = _CreateCharFactory.SpawnCharacter(player);
            _CharacterDic.Add(playerId, character);
            Debug.Log("AddPlayer");
        }
    }
    private void OnPlayerChanged(object sender, ChildChangedEventArgs args)
    {
        string playerId = args.Snapshot.Key;

        Player player = ParsePlayer(args.Snapshot);
        _CharacterDic[playerId].UpdatePlayer(player);

    }
    private Player ParsePlayer(DataSnapshot snap)
    {
        string id = snap.Key;
        string name = snap.Child("playerName").Value.ToString();
        var spriteUrl = snap.Child("charImg").Value.ToString();
        string spriteName = Path.GetFileNameWithoutExtension(spriteUrl);

        float r = System.Convert.ToSingle(snap.Child("r").Value);
        float g = System.Convert.ToSingle(snap.Child("g").Value);
        float b = System.Convert.ToSingle(snap.Child("b").Value);

        Color color = new Color(r / 255f, g / 255f, b / 255f);

        Player p = new Player(id, name,spriteName,color)
        {
            _IsShooting = snap.Child("bullet").Value != null &&
                          (bool)snap.Child("bullet").Value,

            _IsStamping = snap.Child("stamp").Value != null &&
                          (bool)snap.Child("stamp").Value,

            _IsDrawing = snap.Child("draw").Value != null &&
                         (bool)snap.Child("draw").Value
        };

        string direction = "";
        if(snap.Child("direction").Value!=null)
        {
            direction = snap.Child("direction").Value.ToString() ;
            p._Direction = direction ;
        }
        Debug.Log($"[FirebaseDataManager] ParsePlayer() id:{id},name:{name},color:{color},sprite:{spriteName},direction:{direction}");
        return p;
    }
}



