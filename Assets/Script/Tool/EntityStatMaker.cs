using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStatMaker : MonoBehaviour
{
    [SerializeField] public int  entityID;
    [SerializeField] public Stat baseStat;
    [SerializeField] public Stat bonusStat;


    private void Awake()
    {
        baseStat = new Stat();
        bonusStat = new Stat();
    }

    public EntityData GetEntityData()
    {
        EntityData entityData = new EntityData();
        entityData.entityID    = entityID;
        entityData.baseStat  = baseStat;
        entityData.bonusStat = bonusStat;

        return entityData;
    }
    public void SetEntityData(EntityData _entityData)
    {
        entityID  = _entityData.entityID;
        baseStat  = _entityData.baseStat;
        bonusStat = _entityData.bonusStat;

        return;
    }
}
