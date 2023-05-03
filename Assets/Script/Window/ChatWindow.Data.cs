using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Firestore;
using Firebase.Database;
using Firebase.Extensions;
using System.IO;
using Newtonsoft.Json;
using System;


public enum ChatType { MY_TALK, OTHER_TALK, NOTICE, MINI_NOTICE, GUIDE, GUILD };
public class ChatInfo
{
    public ChatType     chatType;
    public string       speakerNickname;
    public string       speakerUID;
    public string       content;
    public long         chattedTime;

    public ChatInfo()
    {
        chatType = ChatType.OTHER_TALK;
        speakerNickname = "";
        speakerUID = "";
        content = "None";
        chattedTime = DateTime.Now.Ticks;
    }
    public ChatInfo(ChatType _chatType, string _conent)
    {
        chatType = _chatType;
        speakerNickname = Account.Instance.nickname;
        speakerUID = Account.Instance.uid;
        content = _conent;
        chattedTime = DateTime.Now.Ticks;
    }


}


public partial class ChatWindow : BaseWindow
{
    private Dictionary<string, List<ChatInfo>> chatData;
    private Dictionary<string, object> testData;

    private DatabaseReference currentReference;

    bool chk = false;

    public void InitChatWindow() {
        currentReference = FirebaseDatabase.DefaultInstance.GetReference("Chat").Child(channel.ToString());

        currentReference.ValueChanged += HandleValueChanged;

        ChatInfo chat = new ChatInfo(ChatType.NOTICE, "[" + Account.Instance.nickname + "]님 환영합니다.");
        AddChatToBoard(chat);
    }

    void HandleChildChanged(object sender, ChildChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }

        print("HandleChildChanged ");
        print("HandleChildChanged "+ args.Snapshot.GetValue(false));
        print("HandleChildChanged "+ args.Snapshot.GetRawJsonValue());
        //AddChatToBoard(JsonConvert.DeserializeObject<ChatInfo>(args.Snapshot.GetRawJsonValue()));
    }

    public void HandleChildAdded(object sender, ChildChangedEventArgs args)
    {
        print("HandleChildAdded");
    }
    public void HandleChildMoved(object sender, ChildChangedEventArgs args)
    {
        print("HandleChildMoved");
    }
    public void HandleChildRemoved(object sender, ChildChangedEventArgs args)
    {
        print("HandleChildRemoved");
    }
    public void HandleValueChanged(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }

        if (chk) AddChatToBoard(JsonConvert.DeserializeObject<ChatInfo>(args.Snapshot.GetRawJsonValue()));
        chk = true;
    }

    public void AddChatToBoard(ChatInfo _chatInfo)
    {
        ChatSlot chatSlot = Instantiate(chatPrefab, scrollRect.content).GetComponent<ChatSlot>();
        chatSlot.UpdateChat(_chatInfo);
    }

    public void AddChatToDB(ChatInfo _chatInfo)
    {
        currentReference.SetRawJsonValueAsync(JsonUtility.ToJson(_chatInfo));
    }

    public void AddChatFromInput()
    {
        ChatInfo chat = new ChatInfo(ChatType.OTHER_TALK, inputChat.text);
        inputChat.text = "";
        AddChatToDB(chat);
    }


}
