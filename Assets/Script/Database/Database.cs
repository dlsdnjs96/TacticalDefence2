using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using Firebase.Firestore;
using Firebase;
using Firebase.Database;
using TMPro;
using Firebase.Extensions;

public partial class Database : Singleton<Database>
{
    public FirebaseAuth auth { get; private set; }
    public FirebaseFirestore db { get; private set; }


    [SerializeField] LoginManager loginManager;
    public string uid;

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
        heroInven = new HeroInfo[30];
        LoadHero();
    }
    private void Start()
    {
    }

    public void OnFinishedLoad()
    {
        print("OnFinishedLoad " + heroInven.Length);
    }

    public void RegistGoogle(string _email, string _password)
    {
        auth.CreateUserWithEmailAndPasswordAsync(_email, _password).ContinueWith(
            task => {
                if (!task.IsCanceled && !task.IsFaulted)
                {
                    Debug.Log(_email + "로 회원가입\n");
                }
                else
                    Debug.Log("회원가입 실패\n");
            }
        );
    }


}
