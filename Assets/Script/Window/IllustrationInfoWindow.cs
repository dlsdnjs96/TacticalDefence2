using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Firebase.Firestore;
using Firebase.Extensions;
using TMPro;

public partial class IllustrationInfoWindow : BaseWindow
{
    private DateTime loadedTime;
    public int selectedHeroID;
    Dictionary<int, List<Comment>> commentList;
    [SerializeField] TMP_InputField inputText;
    [SerializeField] GameObject commentPrefab;
    [SerializeField] GameObject content;
    [SerializeField] GameObject heroImage;
    [SerializeField] BaseWindow insertWindow;


    private void Awake()
    {
        InitializeComment();
        LoadJsonData();
        LoadRecommentedComment();
    }

    public override void OpenWindow()
    {
        base.OpenWindow();
        
        //if (loadedTime < DateTime.Now)
        LoadCommentList();
        UpdateComments();
        UpdateStatInfo();
    }


    public bool IsAvailableToComment()
    {
        foreach (Comment comment in commentList[selectedHeroID])
        {
            if (comment.uid == Account.Instance.uid)
                return false;
        }
        return true;
    }

    public void TryToOpenInsertWindow()
    {
        if (IsAvailableToComment())
            insertWindow.OpenWindow();
        else
            Notice.Instance.ShowNotice("이미 댓글을 남기셨습니다.");
    }

    public void InsertComment()
    {
        Comment comment = new Comment();

        comment.nickname = Account.Instance.nickname;
        comment.uid = Account.Instance.uid;
        comment.commentText = inputText.text;
        comment.thumpUpCount = 0;
        comment.commentedTime = DateTime.Now;
        comment.heroID = selectedHeroID;
        inputText.text = "";

        if (!commentList.ContainsKey(selectedHeroID)) commentList[selectedHeroID] = new List<Comment>();
        commentList[selectedHeroID].Add(comment);
        SaveComment(comment);
        UpdateComments();
    }

    public void CancelComment()
    {
        inputText.text = "";
    }

    public void UpdateComments()
    {
        Transform[] children = content.GetComponentsInChildren<Transform>();
        for (int i = 1; i < children.Length; i++)
            GameObject.Destroy(children[i].gameObject);

        foreach (Comment comment in commentList[selectedHeroID])
        {
            CommentSlot commentSlot = Instantiate(commentPrefab, content.transform).GetComponent<CommentSlot>();
            commentSlot.illustrationInfoWindow = this;
            commentSlot.comment = comment;
            commentSlot.UpdateComment();
        }
    }
    private void ClearHeroImage()
    {
        for (int i = 1; i < heroImage.GetComponentsInChildren<Transform>().Length; i++)
            GameObject.Destroy(heroImage.GetComponentsInChildren<Transform>()[i].gameObject);
    }

    public void UpdateStatInfo()
    {
        ClearHeroImage();

        Hero hero = EntityPool.Instance.Get(selectedHeroID) as Hero;
        hero.gameObject.transform.SetParent(heroImage.transform);
        hero.SetReady();
    }
}
