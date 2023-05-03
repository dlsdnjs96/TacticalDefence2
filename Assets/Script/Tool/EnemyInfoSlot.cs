using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class EnemyInfoSlot : MonoBehaviour, IPointerDownHandler
{
    StageMakerManager stageMakerManager;
    public EnemyInfo enemyInfo;

    void Start()
    {
        stageMakerManager = GameObject.Find("StageMakerManager").GetComponent<StageMakerManager>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        stageMakerManager.AddSpawnData(enemyInfo);
    }

    public void SetEnemyInfo(EnemyInfo _enemyInfo)
    {
        enemyInfo = _enemyInfo;
        Enemy enemy = EntityPool.Instance.Get(_enemyInfo.enemyID) as Enemy;
        if (enemy != null)
        {
            enemy.gameObject.transform.SetParent(transform.Find("EnemyImage"));
            enemy.gameObject.transform.localPosition = Vector3.zero;
        }

        transform.Find("Info").GetComponent<TextMeshProUGUI>().text = "ID \t\t: " + _enemyInfo.enemyID + "\n";
        transform.Find("Info").GetComponent<TextMeshProUGUI>().text += "Level \t: " + _enemyInfo.level + "\n";
        transform.Find("Info").GetComponent<TextMeshProUGUI>().text += "Exp \t\t: " + _enemyInfo.exp + "\n";
        transform.Find("Info").GetComponent<TextMeshProUGUI>().text += "Gold \t\t: " + _enemyInfo.gold + "\n";
    }
}
