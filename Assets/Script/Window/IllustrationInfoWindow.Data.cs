using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System;
using Firebase.Firestore;
using Firebase.Extensions;

public class CommentJson
{
    public DateTime loadedTime;
    public List<Comment> commentList;

    public CommentJson() { commentList = new List<Comment>(); }
}

public partial class IllustrationInfoWindow : BaseWindow
{
    public void InitializeComment()
    {
        commentList = new Dictionary<int, List<Comment>>();
        recommendedComments = new Dictionary<int, HashSet<string>>();
        foreach (Hero hero in Resources.LoadAll<Hero>("Hero/"))
        {
            recommendedComments[int.Parse(hero.gameObject.name)] = new HashSet<string>();
            commentList[int.Parse(hero.gameObject.name)] = new List<Comment>();
        }
    }

    public void LoadJsonData()
    {
        if (!File.Exists(Application.dataPath + Constant.JSON_PATH_COMMENT))
        {
            loadedTime = DateTime.MinValue;
            return;
        }

        string data = File.ReadAllText(Application.dataPath + Constant.JSON_PATH_COMMENT);
        CommentJson commentJson = JsonConvert.DeserializeObject<CommentJson>(data);


        loadedTime = commentJson.loadedTime;
        foreach (Comment comment in commentJson.commentList)
        {
            commentList[comment.heroID].Add(comment);
        }
    }

    public void SaveJsonData()
    {
        CommentJson commentJson = new CommentJson();
        commentJson.loadedTime = loadedTime;

        foreach (List<Comment> jsonCommentList in commentList.Values)
        {
            foreach(Comment comment in jsonCommentList)
            {
                commentJson.commentList.Add(comment);
            }
        }

        File.WriteAllText(Application.dataPath + Constant.JSON_PATH_COMMENT, JsonConvert.SerializeObject(commentJson, Formatting.Indented));
    }
    public void LoadCommentList()
    {

        CollectionReference usersRef = FirebaseFirestore.DefaultInstance.Collection("Comment");

        int heroID;
        Comment tempComment;
        usersRef.WhereGreaterThan("commentedTime", loadedTime).GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            QuerySnapshot snapshots = task.Result;

            foreach (DocumentSnapshot document in snapshots.Documents)
            {
                Dictionary<string, object> documentDictionary = document.ToDictionary();

                if (documentDictionary.ContainsKey("heroID"))
                    heroID = int.Parse(documentDictionary["heroID"].ToString());
                else return;


                tempComment = new Comment();
                tempComment.heroID = heroID;
                tempComment.commentUID = document.Id;
                if (documentDictionary.ContainsKey("nickname")) tempComment.nickname = documentDictionary["nickname"].ToString();
                if (documentDictionary.ContainsKey("uid")) tempComment.uid = documentDictionary["uid"].ToString();
                if (documentDictionary.ContainsKey("commentText")) tempComment.commentText = documentDictionary["commentText"].ToString();
                if (documentDictionary.ContainsKey("thumpUpCount")) tempComment.thumpUpCount = int.Parse(documentDictionary["thumpUpCount"].ToString());
                if (documentDictionary.ContainsKey("commentedTime")) tempComment.commentedTime = ((Timestamp)(documentDictionary["commentedTime"])).ToDateTime();

                commentList[heroID].Add(tempComment);
            }
            loadedTime = DateTime.Now;
            UpdateComments();
            SaveJsonData();
        });
    }

    public void SaveComment(Comment _comment)
    {
        DocumentReference docRef = FirebaseFirestore.DefaultInstance.Collection("Comment").Document();
        Dictionary<string, object> user = new Dictionary<string, object>
        {
                { "heroID", _comment.heroID },
                { "nickname", Account.Instance.nickname },
                { "uid", Account.Instance.uid },
                { "commentText", _comment.commentText },
                { "thumpUpCount", _comment.thumpUpCount },
                { "commentedTime", Timestamp.FromDateTime(_comment.commentedTime) },
        };
        docRef.SetAsync(user);
    }
    public void UpdateThumpUp(Comment _comment)
    {
        DocumentReference docRef = FirebaseFirestore.DefaultInstance.Collection("Comment").Document(_comment.commentUID);
        Dictionary<string, object> user = new Dictionary<string, object>
        {
                { "thumpUpCount", _comment.thumpUpCount },
        };
        docRef.UpdateAsync(user);
    }
}
