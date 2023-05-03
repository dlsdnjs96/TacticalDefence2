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
using System.IO;


public struct EmailPassword
{
    public bool remember;
    public string company;
    public string email;
    public string password;
}

public delegate void OnFinished();

public partial class LoginManager : MonoBehaviour
{

    [SerializeField] TMP_InputField emailField;
    [SerializeField] TMP_InputField passField;
    [SerializeField] Database database;
    [SerializeField] Toggle rememberLogin;


    private EmailPassword emailPassword;

    void Awake()
    {
        emailPassword = new EmailPassword();
        //print("Account.Instance "+ Account.Instance);
    }

    void Start()
    {
        LoadEmailPassword();
        LoginGoogle(emailField.text, passField.text);
    }
    public void LoginButton()
    {
        LoginGoogle(emailField.text, passField.text);
    }
    public void RegistButton()
    {
        database.RegistGoogle(emailField.text, passField.text);
    }


    public void SaveEmailPassword(string _AccountCompany)
    {
        emailPassword.remember = rememberLogin.isOn;
        emailPassword.company = _AccountCompany;
        emailPassword.email = emailField.text;
        emailPassword.password = passField.text;

        File.WriteAllText(Application.dataPath + Constant.JSON_PATH_EMAIL_PW, JsonConvert.SerializeObject(emailPassword, Formatting.Indented));
    }
    public void LoadEmailPassword()
    {
        if (!File.Exists(Application.dataPath + Constant.JSON_PATH_EMAIL_PW)) return;

        string data = File.ReadAllText(Application.dataPath + Constant.JSON_PATH_EMAIL_PW);
        emailPassword = JsonConvert.DeserializeObject<EmailPassword>(data);

        if (emailPassword.remember)
        {
            emailField.text = emailPassword.email;
            passField.text = emailPassword.password;
        }
    }
    public void LoginGoogle(string _email, string _password)
    {
        Database.Instance.uid = "";

        FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync(_email, _password).ContinueWith(
            task => {

                if (task.IsCompleted && !task.IsFaulted && !task.IsCanceled)
                {
                    Database.Instance.uid = task.Result.UserId;
                    Debug.Log(_email + " 로 로그인 하셨습니다.");
                    OnLoadAccount();
                    return;
                }
                else
                {
                    Debug.Log("로그인에 실패하셨습니다.");
                    ShowNotice("fail to login");
                    return;
                }
            }
        );
    }

    public void OnLoadAccount()
    {
        Account.Instance.LoadAccount(Database.Instance.uid, OnLoadedAccount);
    }
    public void OnLoadedAccount()
    {
        if (Account.Instance.nickname == "")
            OpenNicknameWindow();
        else
            LoadRobbyScene();
        SaveEmailPassword("Google");
    }
}
