using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using Firebase.Firestore;
using Firebase;
using Firebase.Database;
using TMPro;
using Firebase.Extensions;

public partial class Database : Singleton<Database>
{
    public static HeroInfo[] heroInven;


    public HeroInfo GetHeroInfoByUID(string _heroUID)
    {
        for (int i = 0; i < heroInven.Length; i++)
        {
            if (heroInven[i] != null && heroInven[i].heroUID == _heroUID)
                return heroInven[i];
        }
        return null;
    }



    public void MakeHero(HeroInfo _heroInfo)
    {
        print("start to make hero");
        DocumentReference docRef = FirebaseFirestore.DefaultInstance.Collection("Hero").Document();
        Dictionary<string, object> user = new Dictionary<string, object>
        {
                { "account", uid },
                { "heroID", _heroInfo.heroID },
                { "enhancement", _heroInfo.enhancement },
                { "level", _heroInfo.level },
                { "exp", _heroInfo.exp },
        };

        docRef.SetAsync(user).ContinueWithOnMainThread(task => {
            _heroInfo.heroUID = docRef.Id;
            print("made document id " + docRef.Id);
        });
    }
    public void RemoveHero(HeroInfo[] _heroSlot, int _slotNumber)
    {
        FirebaseFirestore.DefaultInstance.Collection("Hero").Document(_heroSlot[_slotNumber].heroUID).DeleteAsync();
        _heroSlot[_slotNumber] = null;
    }
    public void LoadHero()
    {
        // Hero Collection�� ����  
        CollectionReference usersRef = FirebaseFirestore.DefaultInstance.Collection("Hero");

        // �������� uid�� ���� �����͸� ��û
        usersRef.WhereEqualTo("account", uid).GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            QuerySnapshot snapshots = task.Result;

            int slotNumber = 0;
            // ���ǿ� �´� Document�� ��ȸ
            foreach (DocumentSnapshot document in snapshots.Documents)
            {
                // Document�� Dictionary���·� �ޱ�
                Dictionary<string, object> documentDictionary = document.ToDictionary();

                // �ҷ��� Document�����͸� heroInven�� ����
                heroInven[slotNumber] = new HeroInfo();
                if (documentDictionary.ContainsKey("heroID")) heroInven[slotNumber].heroID = int.Parse(documentDictionary["heroID"].ToString());
                if (documentDictionary.ContainsKey("enhancement")) heroInven[slotNumber].enhancement = int.Parse(documentDictionary["enhancement"].ToString());
                if (documentDictionary.ContainsKey("level")) heroInven[slotNumber].level = int.Parse(documentDictionary["level"].ToString());
                if (documentDictionary.ContainsKey("exp")) heroInven[slotNumber].exp = int.Parse(documentDictionary["exp"].ToString());
                heroInven[slotNumber].heroUID = document.Id;
                slotNumber++;
            }
        });
    }
    public void SaveAllHero()
    {
        for (int i = 0; i < heroInven.Length; i++)
        {
            if (heroInven[i] != null)
                SaveHero(heroInven[i]);
        }
    }
    public void SaveHero(HeroInfo _heroInfo)
    {
        DocumentReference docRef = FirebaseFirestore.DefaultInstance.Collection("Hero").Document(_heroInfo.heroUID);
        Dictionary<string, object> user = new Dictionary<string, object>
        {
                { "account", uid },
                { "heroID", _heroInfo.heroID },
                { "enhancement", _heroInfo.enhancement },
                { "level", _heroInfo.level },
                { "exp", _heroInfo.exp },
        };
        docRef.SetAsync(user);
    }
}
