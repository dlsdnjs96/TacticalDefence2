using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;


[System.Serializable]
public struct SpawnData
{
    public int   enemyId;
    public int   enemyLevel;
    public float spawnTime;
    public float duration;
    public short repeatCount;
    public short enemyCount;
}

[System.Serializable]
public class StageSpawnData
{
    public int stageNumber;
    public int enemyCount;
    public List<SpawnData> data;

    public StageSpawnData() { data = new List<SpawnData>(); }
}

public class SpawnDataJson
{
    public List<StageSpawnData> list;
    public SpawnDataJson() { list = new List<StageSpawnData>(); }
}


public partial class StageManager : MonoBehaviour
{
    private int currentStage;
    private Dictionary<int, StageSpawnData> spawnData;


    private void Test()
    {
        spawnData = new Dictionary<int, StageSpawnData>();

        for (int i = 1; i <= 3; i++)
        {
            spawnData[i] = new StageSpawnData();
            SpawnData _data = new SpawnData();

            spawnData[i].stageNumber = i;
            _data.enemyId = 1000000;
            _data.enemyLevel = 1;
            _data.spawnTime = 1f;
            _data.duration = 2f;
            _data.repeatCount = (short)(2 + i);
            _data.enemyCount = (short)(1 + i);
            spawnData[i].data.Add(_data);


            SpawnData _data2 = new SpawnData();

            _data2.enemyId = 1000000;
            _data.enemyLevel = 1;
            _data2.spawnTime = 2f;
            _data2.duration = 2f;
            _data2.repeatCount = (short)(3 + i);
            _data2.enemyCount = (short)(2 + i);
            spawnData[i].data.Add(_data2);
        }
    }
    private void SaveData()
    {
        SpawnDataJson data = new SpawnDataJson();

        foreach (StageSpawnData it in spawnData.Values)
        {
            data.list.Add(it);
        }

            
        File.WriteAllText(Application.dataPath + Constant.JSON_PATH_SPAWN, JsonConvert.SerializeObject(data, Formatting.Indented));
    }
    private void LoadData()
    {
        string data = File.ReadAllText(Application.dataPath + Constant.JSON_PATH_SPAWN);
        SpawnDataJson spawnDataJson = JsonConvert.DeserializeObject<SpawnDataJson>(data);
        spawnData = new Dictionary<int, StageSpawnData>();

        foreach (StageSpawnData it in spawnDataJson.list)
        {
            spawnData[it.stageNumber] = it;
            spawnData[it.stageNumber].enemyCount = 0;
            foreach (SpawnData it2 in it.data)
                spawnData[it.stageNumber].enemyCount += it2.enemyCount * it2.repeatCount;
        }
    }

}
