using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;
using Firebase.Extensions;
using System.IO;

public partial class IllustrationInfoWindow : BaseWindow
{
    private Dictionary<int, HashSet<string>> recommendedComments;
    public void LoadRecommentedComment()
    {
        CollectionReference usersRef = FirebaseFirestore.DefaultInstance.Collection("RecommendedComments");

        usersRef.WhereEqualTo("accountID", Account.Instance.uid).GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            QuerySnapshot snapshots = task.Result;

            foreach (DocumentSnapshot document in snapshots.Documents)
            {
                Dictionary<string, object> documentDictionary = document.ToDictionary();

                if (!documentDictionary.ContainsKey("heroID") || !documentDictionary.ContainsKey("commentUID"))
                    break;

                recommendedComments[int.Parse(documentDictionary["heroID"].ToString())].Add((string)documentDictionary["commentUID"]);
            }
        });
    }
    public void SaveRecommentedComment(Comment _comment)
    {
        DocumentReference docRef = FirebaseFirestore.DefaultInstance.Collection("RecommendedComments").Document();
        Dictionary<string, object> user = new Dictionary<string, object>
        {
                { "accountID", Account.Instance.uid },
                { "heroID", _comment.heroID },
                { "commentUID", _comment.uid },
        };
        docRef.SetAsync(user);
    }

    public bool IsAvailableToRecommend(Comment _comment)
    {
        if (recommendedComments[_comment.heroID].Contains(_comment.uid))
            return false;
        return true;
    }
    public void RecommendComment(Comment _comment)
    {
        recommendedComments[_comment.heroID].Add(_comment.uid);
    }
}
