using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public partial class ChatWindow : BaseWindow
{
    [SerializeField] ScrollRect scrollRect;

    [SerializeField] RectTransform allChat;
    [SerializeField] RectTransform guideChat;
    [SerializeField] RectTransform talkChat;
    [SerializeField] RectTransform noticeChat;

    public void CloseAllChat()
    {
        allChat.gameObject.SetActive(false);
        guideChat.gameObject.SetActive(false);
        talkChat.gameObject.SetActive(false);
        noticeChat.gameObject.SetActive(false);
    }

    public void OpenAllChat()
    {
        CloseAllChat();
        allChat.gameObject.SetActive(true);
        scrollRect.content = allChat;
    }
    public void OpenGuideChat()
    {
        CloseAllChat();
        guideChat.gameObject.SetActive(true);
        scrollRect.content = guideChat;
    }
    public void OpenTalkChat()
    {
        CloseAllChat();
        talkChat.gameObject.SetActive(true);
        scrollRect.content = talkChat;
    }
    public void OpenNoticeChat()
    {
        CloseAllChat();
        noticeChat.gameObject.SetActive(true);
        scrollRect.content = noticeChat;
    }
}
