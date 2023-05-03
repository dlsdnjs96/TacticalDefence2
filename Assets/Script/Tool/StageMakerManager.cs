using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StageMakerManager : MonoBehaviour
{
    StageMaker stageMaker;
    [SerializeField] TextMeshProUGUI StageInfo;


    [SerializeField] GameObject StagePreFab;
    [SerializeField] GameObject SpawnPreFab;
    [SerializeField] GameObject EnemyPreFab;
    [SerializeField] GameObject StageContent;
    [SerializeField] GameObject SpawnContent;
    [SerializeField] GameObject EnemyContent;

    void Start()
    {
        LoadEnemyData();
        LoadStageData();
    }


    public void LoadStageData()
    {
        print(StatInformation.Instance);
        StageInformation.Instance.LoadInformation();

        foreach (StageSpawnData stageData in StageInformation.info)
        {
            print("stageData " + stageData.stageNumber.ToString());
            Instantiate(StagePreFab, StageContent.transform).GetComponent<StageMaker>().SetData(stageData);
        }
    }

    public void SaveStageData()
    {
        StageInformation.info.Clear();

        foreach (StageMaker maker in StageContent.transform.GetComponentsInChildren<StageMaker>())
        {
            StageInformation.info.Add(maker.stageSpawnData);
        }
        StageInformation.Instance.SaveInformation();
    }

    public void LoadEnemyData()
    {
        EnemyInformation.Instance.LoadInformation();

        foreach (Dictionary<int, EnemyInfo> enemyInfoList in EnemyInformation.info.Values)
        {
            foreach (EnemyInfo enemyInfo in enemyInfoList.Values)
            {
                Instantiate(EnemyPreFab, EnemyContent.transform).GetComponent<EnemyInfoSlot>().SetEnemyInfo(enemyInfo);
            }
        }
    }

    public void SaveEnemyData()
    {
        StatInformation.info.Clear();

        foreach (EntityStatMaker heroStatMaker in transform.GetComponentsInChildren<EntityStatMaker>())
        {
            EntityData heroData = heroStatMaker.GetEntityData();
            StatInformation.info.Add(heroData.entityID, heroData);
        }
        StatInformation.Instance.SaveStatInformation();
    }

    public void ShowStageInfo(StageMaker _stageMaker)
    {
        stageMaker = _stageMaker;
        for (int i = 0; i < SpawnContent.transform.childCount; i++)
            GameObject.Destroy(SpawnContent.transform.GetChild(i).gameObject);


        foreach (SpawnData iter in stageMaker.stageSpawnData.data)
        {
            Instantiate(SpawnPreFab, SpawnContent.transform).GetComponent<SpawnDataMaker>().UpdateSpawnData(iter);
        }
        UpdateInfo();
    }

    public void UpdateInfo()
    {
        int totalExp = 0, totalGold = 0;
        //foreach (SpawnData iter in stageMaker.stageSpawnData.data)
        //{
        //    EnemyInfo enemyInfo = EnemyInformation.Instance.GetEnemyInfo(iter.enemyId, iter.enemyLevel);
        //    if (enemyInfo == null) continue;
        //
        //    totalExp += EnemyInformation.Instance.GetEnemyInfo(iter.enemyId, iter.enemyLevel).exp;
        //    totalGold += EnemyInformation.Instance.GetEnemyInfo(iter.enemyId, iter.enemyLevel).gold;
        //}
        print("UpdateInfo "+ SpawnContent.transform.childCount);
        foreach (SpawnDataMaker maker in SpawnContent.transform.GetComponentsInChildren<SpawnDataMaker>())
        {
            print("maker.spawnData.enemyLevel " + maker.spawnData.enemyLevel);
            EnemyInfo enemyInfo = EnemyInformation.Instance.GetEnemyInfo(maker.spawnData.enemyId, maker.spawnData.enemyLevel);
            if (enemyInfo == null) continue;

            totalExp += enemyInfo.exp * maker.spawnData.enemyCount * maker.spawnData.repeatCount;
            totalGold += enemyInfo.gold * maker.spawnData.enemyCount * maker.spawnData.repeatCount;
        }

        StageInfo.text = "Stage " + stageMaker.stageSpawnData.stageNumber.ToString() + "\n";
        StageInfo.text += "TotalExp " + totalExp.ToString() + "\n";
        StageInfo.text += "TotalGold " + totalGold.ToString();
    }

    public void AddSpawnData(EnemyInfo _enemyInfo)
    {
        SpawnData spawnData = new SpawnData();
        spawnData.enemyId = _enemyInfo.enemyID;
        spawnData.enemyLevel = _enemyInfo.level;
        Instantiate(SpawnPreFab, SpawnContent.transform).GetComponent<SpawnDataMaker>().UpdateSpawnData(spawnData);
        UpdateInfo();
    }
}
