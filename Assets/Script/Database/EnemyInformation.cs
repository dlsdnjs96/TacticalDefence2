using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;


public class EnemyInfoJson
{
    public List<EnemyInfo> list;
}


public partial class EnemyInformation : Singleton<EnemyInformation>
{
    public static Dictionary<int, Dictionary<int, EnemyInfo>> info;

    void Awake()
    {
        info = new Dictionary<int, Dictionary<int, EnemyInfo>>();
        LoadInformation();
    }
    public void LoadInformation()
    {
        string data = File.ReadAllText(Application.dataPath + Constant.JSON_PATH_ENEMY_INFO);
        EnemyInfoJson enemyInfoJson = JsonConvert.DeserializeObject<EnemyInfoJson>(data);

        foreach (EnemyInfo enemyInfo in enemyInfoJson.list)
        {
            SetEnemyInfo(enemyInfo);
        }
    }

    public void SaveInformation()
    {
        EnemyInfoJson enemyInfoJson = new EnemyInfoJson();
        enemyInfoJson.list = new List<EnemyInfo>();


        foreach (Dictionary<int, EnemyInfo> enemyInfoList in info.Values)
        {
            foreach (EnemyInfo enemyInfo in enemyInfoList.Values)
                enemyInfoJson.list.Add(enemyInfo);
        }

        File.WriteAllText(Application.dataPath + Constant.JSON_PATH_ENEMY_INFO, JsonConvert.SerializeObject(enemyInfoJson, Formatting.Indented));
    }

    public void SetEnemyInfo(EnemyInfo _enemyInfo)
    {
        if (!info.ContainsKey(_enemyInfo.enemyID)) 
            info[_enemyInfo.enemyID] = new Dictionary<int, EnemyInfo>();
        info[_enemyInfo.enemyID][_enemyInfo.level] = _enemyInfo; 
    }
    public EnemyInfo GetEnemyInfo(int _enemyID, int _level)
    {
        if (!info.ContainsKey(_enemyID) || !info[_enemyID].ContainsKey(_level))
            return null;
        return info[_enemyID][_level];
    }
}
