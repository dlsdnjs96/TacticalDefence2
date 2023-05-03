using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class StageMaker : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] public StageSpawnData stageSpawnData;
    StageMakerManager stageMakerManager;

    private void Awake()
    {
        stageMakerManager = GameObject.Find("StageMakerManager").GetComponent<StageMakerManager>();
        //stageSpawnData = new StageSpawnData();
    }

    public void SetData(StageSpawnData _stageSpawnData)
    {
        stageSpawnData = _stageSpawnData;
        UpdateData();
        return;
    }

    public void UpdateData()
    {
        print("UpdateData " + stageSpawnData.stageNumber);
        transform.Find("Name").GetComponent<TextMeshProUGUI>().text = "Stage "+stageSpawnData.stageNumber;
        //
        //int.TryParse(id.text, out enemyInfo.enemyID);
        //int.TryParse(level.text, out enemyInfo.level);
        //int.TryParse(gold.text, out enemyInfo.gold);
        //int.TryParse(exp.text, out enemyInfo.exp);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        stageMakerManager.ShowStageInfo(this);
    }
}
