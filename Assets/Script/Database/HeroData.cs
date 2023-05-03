using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class EntityData
{
    public int  entityID;
    public Stat baseStat;
    public Stat bonusStat;

    public EntityData()
    {
        baseStat  = new Stat();
        bonusStat = new Stat();
    }
}

public class EntityDataJson
{
    public List<EntityData> list;
}

