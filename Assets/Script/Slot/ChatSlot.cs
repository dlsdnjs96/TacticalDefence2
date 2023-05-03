using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Firebase.Database;
using Firebase.Extensions;

public class ChatSlot : MonoBehaviour
{
    private TextMeshProUGUI content;
    public DateTime chattedTime;

    private void Awake()
    {
        content = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateChat(ChatInfo _chatInfo)
    {
        if (_chatInfo.chatType == ChatType.OTHER_TALK && _chatInfo.speakerUID == Account.Instance.uid) _chatInfo.chatType = ChatType.MY_TALK;

        switch (_chatInfo.chatType)
        {
            case ChatType.MY_TALK:
                content.text = "["+ _chatInfo.speakerNickname+ "] : " + _chatInfo.content;
                content.color = new Color(40, 40, 40);
                break;
            case ChatType.OTHER_TALK:
                content.text = "[" + _chatInfo.speakerNickname + "] : " + _chatInfo.content;
                content.color = new Color(75, 75, 75);
                break;
            case ChatType.NOTICE:
                content.text = "<b>[공지] : " + _chatInfo.content+"</b>";
                content.color = Color.yellow;
                break;
            case ChatType.MINI_NOTICE:
                content.text = "<i>"+_chatInfo.content+ "</i>";
                content.color = Color.gray;
                break;
            case ChatType.GUIDE:
                content.text = "<b>[가이드] : " + _chatInfo.content + "</b>";
                content.color = Color.blue;
                break;
            case ChatType.GUILD:
                content.color = Color.blue;
                break;
        }
    }
}
