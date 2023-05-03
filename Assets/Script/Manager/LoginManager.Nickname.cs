using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Auth;
using Firebase.Firestore;
using Firebase;
using Firebase.Database;
using TMPro;
using Firebase.Extensions;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;
using System;



public partial class LoginManager : MonoBehaviour
{
    public void ConfirmNicknameButton()
    {
        CheckAvailableNickName();
    }

    public void DoubleCheckYesButton()
    {
        Account.Instance.nickname = nicknameInputField.text;
        Account.Instance.createdTime = Timestamp.GetCurrentTimestamp();
        Account.Instance.SaveAccount(LoadRobbyScene);
    }

    public void LoadRobbyScene()
    {
        SceneManager.LoadScene("Robby");
        Database.Instance.LoadHero();
    }
    public void DoubleCheckNoButton()
    {
        CloseDoubleCheckWindow();
    }

    public void CheckAvailableNickName()
    {
        CollectionReference usersRef = FirebaseFirestore.DefaultInstance.Collection("Account");

        usersRef.WhereEqualTo("nickname", nicknameInputField.text).GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            QuerySnapshot documentSnapshots = task.Result;

            if (documentSnapshots.Count <= 0)
            {
                doubleCheckText.text = "Are you sure to use [" + nicknameInputField.text + "] as your nickname?";
                OpenDoubleCheckWindow();
            }
            else
            {
                noticeText.text = nicknameInputField.text + " is not available.";
                OpenNoticeWindow();
            }
            return;
        });
        return;
    }

}
