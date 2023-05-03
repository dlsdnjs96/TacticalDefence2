using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Firebase.Database;

public partial class ChatWindow : BaseWindow
{
    private int channel;
    [SerializeField] TMP_InputField inputChat;
    [SerializeField] TMP_InputField inputChannel;
    [SerializeField] GameObject chatPrefab;
    [SerializeField] TextMeshProUGUI channelButtonText;

    private void Awake()
    {
        channel = 1;
        InitChatWindow();
        UpdateChannelButtonText();
    }

    public void OpenKeyBoard()
    {
        print("OpenKeyBoard");
        TouchScreenKeyboard touchKeyBoard = new TouchScreenKeyboard("aa", TouchScreenKeyboardType.Default, false, false, false, false, "test", 100);
        
        print("TouchScreenKeyboard.visible " + TouchScreenKeyboard.visible);
    }
    public void TryChangeChannel()
    {
        if (int.TryParse(inputChannel.text, out channel) && channel >= 1 && channel <= 100)
        {
            ChangeChannel();
            UpdateChannelButtonText();
            AddChatToBoard(new ChatInfo(ChatType.MINI_NOTICE, inputChannel.text + " 채널에 접속하였습니다."));
            inputChannel.gameObject.GetComponentInParent<BaseWindow>().CloseWindow();
        } else
        {
            Notice.Instance.ShowNotice("1이상 100이하의 숫자를 입력해주세요.");
        }
    }

    private void UpdateChannelButtonText()
    {
        channelButtonText.text = "채널 " + inputChannel.text;
    }

    public void ChangeChannel()
    {
        currentReference.ChildChanged -= HandleChildChanged;
        currentReference = FirebaseDatabase.DefaultInstance.GetReference("Chat").Child(inputChat.text);
        currentReference.ChildChanged += HandleChildChanged;
    }

    public void AddNoticeChat(string _text)
    {
        ChatInfo chatInfo = new ChatInfo();
        chatInfo.content = _text;
        AddChatToBoard(chatInfo);
    }
}
