using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public partial class StatInformation : Singleton<StatInformation>
{
    public static Dictionary<int, EntityData> info;

    void Awake()
    {
        info = new Dictionary<int, EntityData>();
        LoadStatInformation();
    }
    public void LoadStatInformation()
    {
        string data = File.ReadAllText(Application.dataPath + Constant.JSON_PATH_STAT_DATA);
        EntityDataJson entityDataJson = JsonConvert.DeserializeObject<EntityDataJson>(data);

        foreach(EntityData entityData in entityDataJson.list)
        {
            info[entityData.entityID] = entityData;
        }
    }

    public void SaveStatInformation()
    {
        EntityDataJson entityDataJson = new EntityDataJson();
        entityDataJson.list = new List<EntityData>();


        foreach (EntityData entityData in info.Values)
            entityDataJson.list.Add(entityData);

        File.WriteAllText(Application.dataPath + Constant.JSON_PATH_STAT_DATA, JsonConvert.SerializeObject(entityDataJson, Formatting.Indented));
    }
}
