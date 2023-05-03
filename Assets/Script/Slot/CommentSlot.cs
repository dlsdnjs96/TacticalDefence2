using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Firebase.Firestore;
using Firebase.Extensions;
using System;

public class Comment
{
    public string commentUID;
    public string nickname;
    public string uid;
    public string commentText;
    public int thumpUpCount;
    public int heroID;
    public DateTime commentedTime;
}

public class CommentSlot : MonoBehaviour
{
    public Comment comment;

    [SerializeField] TextMeshProUGUI nicknameTMP;
    [SerializeField] TextMeshProUGUI commentTextTMP;
    [SerializeField] TextMeshProUGUI thumpUpCountTMP;
    [SerializeField] TextMeshProUGUI commentedTimeTMP;
    public IllustrationInfoWindow illustrationInfoWindow;

    public void UpdateComment()
    {
        nicknameTMP.text = comment.nickname;
        commentTextTMP.text = comment.commentText;
        thumpUpCountTMP.text = comment.thumpUpCount.ToString();
        commentedTimeTMP.text = comment.commentedTime.ToString("yyyy-MM-dd");
    }

    public void TryRecommend()
    {

        if (comment.commentUID == Account.Instance.uid)
        {
            Notice.Instance.ShowNotice("자신의 댓글은 추천이 불가능합니다.");
            return;
        }
        if (!illustrationInfoWindow.IsAvailableToRecommend(comment))
        {
            Notice.Instance.ShowNotice("이미 추천하셨습니다.");
            return;
        }
        comment.thumpUpCount++;
        thumpUpCountTMP.text = comment.thumpUpCount.ToString();
        illustrationInfoWindow.RecommendComment(comment);
        illustrationInfoWindow.SaveRecommentedComment(comment);
        //illustrationInfoWindow.UpdateThumpUp(comment);
    }
}
