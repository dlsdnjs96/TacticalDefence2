using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;



public partial class StageInformation : Singleton<StageInformation>
{
    public static List<StageSpawnData> info;

    void Awake()
    {
        info = new List<StageSpawnData>();
        //LoadInformation();
    }
    public void LoadInformation()
    {
        info.Clear();
        string data = File.ReadAllText(Application.dataPath + Constant.JSON_PATH_SPAWN);
        SpawnDataJson spawnDataJson = JsonConvert.DeserializeObject<SpawnDataJson>(data);

        foreach (StageSpawnData stageSpawnData in spawnDataJson.list)
        {
            info.Add(stageSpawnData);
        }
    }

    public void SaveInformation()
    {
        SpawnDataJson spawnDataJson = new SpawnDataJson();


        foreach (StageSpawnData stageSpawnData in info)
        {
            spawnDataJson.list.Add(stageSpawnData);
        }

        File.WriteAllText(Application.dataPath + Constant.JSON_PATH_SPAWN, JsonConvert.SerializeObject(spawnDataJson, Formatting.Indented));
    }

}
