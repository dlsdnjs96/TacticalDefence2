using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Firebase.Firestore;
using Firebase.Extensions;

public class Account : Singleton<Account>
{
   [SerializeField] public string       uid { get; private set; }
    public string       nickname;
    public int          gold { get; private set; }
    public int          ruby { get; private set; }
    public Timestamp    createdTime;// { get; private set; }
    public Timestamp    lastLogin { get; private set; }

    void Awake()
    {
        DontDestroyOnLoad(this);
        instance = this;
    }
    public Account()
    {
        uid = "";
        nickname = "";
        gold = 0;
        ruby = 0;
        createdTime = Timestamp.GetCurrentTimestamp();
        lastLogin = Timestamp.GetCurrentTimestamp();
    }


    public void LoadAccount(string _uid, OnFinished onLoaded)
    {
        uid = _uid;
        CollectionReference usersRef = FirebaseFirestore.DefaultInstance.Collection("Account");


        usersRef.Document(uid).GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            DocumentSnapshot document = task.Result;


            if (document.Exists)
            {
                Dictionary<string, object> documentDictionary = document.ToDictionary();

                if (documentDictionary.ContainsKey("nickname")) nickname = (string)documentDictionary["nickname"];
                if (documentDictionary.ContainsKey("gold")) gold = int.Parse(documentDictionary["gold"].ToString());
                if (documentDictionary.ContainsKey("ruby")) ruby = int.Parse(documentDictionary["ruby"].ToString());
                if (documentDictionary.ContainsKey("createdTime")) createdTime = (Timestamp)documentDictionary["createdTime"];
                if (documentDictionary.ContainsKey("lastLogin")) lastLogin = (Timestamp)documentDictionary["lastLogin"];
            }
            onLoaded();
        });
    }

    public void SaveAccount(OnFinished onFinished)
    {
        DocumentReference docRef = FirebaseFirestore.DefaultInstance.Collection("Account").Document(uid);
        Dictionary<string, object> user = new Dictionary<string, object>
        {
                { "nickname", nickname },
                { "gold", gold },
                { "ruby", ruby },
                { "createdTime", createdTime },
                { "lastLogin", Timestamp.GetCurrentTimestamp() },
        };
        docRef.SetAsync(user).ContinueWithOnMainThread(task => {
            onFinished();
            Debug.Log("Added data to the alovelace document in the users collection.");
        });
    }


}
