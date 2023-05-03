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
    public static Equipment[] equipInven;






    public void MakeItem(Equipment _equipment)
    {
        DocumentReference docRef = FirebaseFirestore.DefaultInstance.Collection("Equipment").Document();
        Dictionary<string, object> user = new Dictionary<string, object>
        {
                { "account", uid },
                { "itemID", _equipment.itemID },
                { "enhancement", _equipment.enhancement },
        };
        if (_equipment.stat.mAtk > 0f)              user["mAtk"]                = _equipment.stat.mAtk;
        if (_equipment.stat.wAtk > 0f)              user["wAtk"]                = _equipment.stat.wAtk;
        if (_equipment.stat.atkSpeed > 0f)          user["atkSpeed"]            = _equipment.stat.atkSpeed;
        if (_equipment.stat.atkRange > 0f)          user["atkRange"]            = _equipment.stat.atkRange;
        if (_equipment.stat.criProb > 0f)           user["criProb"]             = _equipment.stat.criProb;
        if (_equipment.stat.criDamage > 0f)         user["criDamage"]           = _equipment.stat.criDamage;
        if (_equipment.stat.maxHp > 0f)             user["maxHp"]               = _equipment.stat.maxHp;
        if (_equipment.stat.defensive > 0f)         user["defensive"]           = _equipment.stat.defensive;
        if (_equipment.stat.coolTimeReduction > 0f) user["coolTimeReduction"]   = _equipment.stat.coolTimeReduction;
        if (_equipment.stat.physicalLifeSteal > 0f) user["physicalLifeSteal"]   = _equipment.stat.physicalLifeSteal;
        if (_equipment.stat.magicalLifeSteal > 0f)  user["magicalLifeSteal"]    = _equipment.stat.magicalLifeSteal;
        if (_equipment.stat.physicalDamage > 0f)    user["physicalDamage"]      = _equipment.stat.physicalDamage;
        if (_equipment.stat.magicalDamage > 0f)     user["magicalDamage"]       = _equipment.stat.magicalDamage;



        docRef.SetAsync(user).ContinueWithOnMainThread(task => {
        });
    }
    public void RemoveItem(HeroInfo[] _heroSlot, int _slotNumber)
    {
        FirebaseFirestore.DefaultInstance.Collection("Hero").Document(_heroSlot[_slotNumber].heroUID).DeleteAsync();
        _heroSlot[_slotNumber] = null;
    }
    public void LoadItem()
    {
        if (equipInven == null) equipInven = new Equipment[30];

        CollectionReference usersRef = FirebaseFirestore.DefaultInstance.Collection("Hero");

        usersRef.WhereEqualTo("account", uid).GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            QuerySnapshot snapshots = task.Result;

            int slotIndex = 0;
            foreach (DocumentSnapshot document in snapshots.Documents)
            {
                Dictionary<string, object> dictionary = document.ToDictionary();

                equipInven[slotIndex] = new Equipment();
                if (dictionary.ContainsKey("itemID")) equipInven[slotIndex].itemID = int.Parse(dictionary["itemID"].ToString());
                if (dictionary.ContainsKey("enhancement")) equipInven[slotIndex].enhancement = int.Parse(dictionary["enhancement"].ToString());
                

                if (dictionary.ContainsKey("mAtk"))                 equipInven[slotIndex].stat.mAtk                = float.Parse(dictionary["mAtk"].ToString());
                if (dictionary.ContainsKey("wAtk"))                 equipInven[slotIndex].stat.wAtk                = float.Parse(dictionary["wAtk"].ToString());
                if (dictionary.ContainsKey("atkSpeed"))             equipInven[slotIndex].stat.atkSpeed            = float.Parse(dictionary["atkSpeed"].ToString());
                if (dictionary.ContainsKey("atkRange"))             equipInven[slotIndex].stat.atkRange            = float.Parse(dictionary["atkRange"].ToString());
                if (dictionary.ContainsKey("criProb"))              equipInven[slotIndex].stat.criProb             = float.Parse(dictionary["criProb"].ToString());
                if (dictionary.ContainsKey("criDamage"))            equipInven[slotIndex].stat.criDamage           = float.Parse(dictionary["criDamage"].ToString());
                if (dictionary.ContainsKey("maxHp"))                equipInven[slotIndex].stat.maxHp               = float.Parse(dictionary["maxHp"].ToString());
                if (dictionary.ContainsKey("defensive"))            equipInven[slotIndex].stat.defensive           = float.Parse(dictionary["defensive"].ToString());
                if (dictionary.ContainsKey("coolTimeReduction"))    equipInven[slotIndex].stat.coolTimeReduction   = float.Parse(dictionary["coolTimeReduction"].ToString());
                if (dictionary.ContainsKey("physicalLifeSteal"))    equipInven[slotIndex].stat.physicalLifeSteal   = float.Parse(dictionary["physicalLifeSteal"].ToString());
                if (dictionary.ContainsKey("magicalLifeSteal"))     equipInven[slotIndex].stat.magicalLifeSteal    = float.Parse(dictionary["magicalLifeSteal"].ToString());
                if (dictionary.ContainsKey("physicalDamage"))       equipInven[slotIndex].stat.physicalDamage      = float.Parse(dictionary["physicalDamage"].ToString());
                if (dictionary.ContainsKey("magicalDamage"))        equipInven[slotIndex].stat.magicalDamage       = float.Parse(dictionary["magicalDamage"].ToString());
            

                slotIndex++;
            }
            //onLoaded();
        });
    }
    public void SaveAllItems()
    {
    }
    public void SaveItem(Equipment _equipment)
    {
        DocumentReference docRef = FirebaseFirestore.DefaultInstance.Collection("Equipment").Document(uid);
        Dictionary<string, object> user = new Dictionary<string, object>
        {
                { "account", uid },
                { "itemID", _equipment.itemID },
                { "enhancement", _equipment.enhancement },
        };
        if (_equipment.stat.mAtk > 0f) user["mAtk"] = _equipment.stat.mAtk;
        if (_equipment.stat.wAtk > 0f) user["wAtk"] = _equipment.stat.wAtk;
        if (_equipment.stat.atkSpeed > 0f) user["atkSpeed"] = _equipment.stat.atkSpeed;
        if (_equipment.stat.atkRange > 0f) user["atkRange"] = _equipment.stat.atkRange;
        if (_equipment.stat.criProb > 0f) user["criProb"] = _equipment.stat.criProb;
        if (_equipment.stat.criDamage > 0f) user["criDamage"] = _equipment.stat.criDamage;
        if (_equipment.stat.maxHp > 0f) user["maxHp"] = _equipment.stat.maxHp;
        if (_equipment.stat.defensive > 0f) user["defensive"] = _equipment.stat.defensive;
        if (_equipment.stat.coolTimeReduction > 0f) user["coolTimeReduction"] = _equipment.stat.coolTimeReduction;
        if (_equipment.stat.physicalLifeSteal > 0f) user["physicalLifeSteal"] = _equipment.stat.physicalLifeSteal;
        if (_equipment.stat.magicalLifeSteal > 0f) user["magicalLifeSteal"] = _equipment.stat.magicalLifeSteal;
        if (_equipment.stat.physicalDamage > 0f) user["physicalDamage"] = _equipment.stat.physicalDamage;
        if (_equipment.stat.magicalDamage > 0f) user["magicalDamage"] = _equipment.stat.magicalDamage;
        docRef.SetAsync(user);
    }
}
