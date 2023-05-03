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




public partial class LoginManager : MonoBehaviour
{
    [SerializeField] GameObject nicknameWindow;
    [SerializeField] GameObject doubleCheckWindow;
    [SerializeField] GameObject noticeWindow;

    [SerializeField] TMP_InputField nicknameInputField;
    [SerializeField] TextMeshProUGUI doubleCheckText;
    [SerializeField] TextMeshProUGUI noticeText;

    public void OpenNicknameWindow() { nicknameWindow.SetActive(true); }
    public void CloseNicknameWindow() { nicknameWindow.SetActive(false); }
    public void OpenDoubleCheckWindow() { doubleCheckWindow.SetActive(true); }
    public void CloseDoubleCheckWindow() { doubleCheckWindow.SetActive(false); }
    public void OpenNoticeWindow() { noticeWindow.SetActive(true); }
    public void CloseNoticeWindow() { noticeWindow.SetActive(false); }
    public void ShowNotice(string _notice)
    {
        noticeText.text = _notice;
        OpenNoticeWindow();
    }
}
