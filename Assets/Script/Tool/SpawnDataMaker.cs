using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpawnDataMaker : MonoBehaviour
{
    public SpawnData spawnData;
    StageMakerManager stageMakerManager;
    [SerializeField] TextMeshProUGUI enemyID_level;
    [SerializeField] TMP_InputField inputSpawnTime;
    [SerializeField] TMP_InputField inputDuration;
    [SerializeField] TMP_InputField inputRepeatCount;
    [SerializeField] TMP_InputField inputEnemyCount;

    private void Awake()
    {
        spawnData = new SpawnData();
        stageMakerManager = GameObject.Find("StageMakerManager").GetComponent<StageMakerManager>();
    }


    public void UpdateSpawnData(SpawnData _spawnData)
    {
        spawnData = _spawnData;
        Enemy enemy = EntityPool.Instance.Get(_spawnData.enemyId) as Enemy;
        if (enemy != null)
        {
            enemy.gameObject.transform.SetParent(transform.Find("EnemyImage"));
            enemy.gameObject.transform.localPosition = Vector3.zero;
        }
        enemyID_level.text = "ID : " + spawnData.enemyId + ", Lv." + spawnData.enemyLevel;
        //UpdateInput();

        inputSpawnTime.text = spawnData.spawnTime.ToString();
        inputDuration.text = spawnData.duration.ToString();
        inputRepeatCount.text = spawnData.repeatCount.ToString();
        inputEnemyCount.text = spawnData.enemyCount.ToString();
    }

    public void UpdateInput()
    {
        if (!float.TryParse(inputSpawnTime.text, out spawnData.spawnTime)) inputSpawnTime.text = spawnData.spawnTime.ToString();
        if (!float.TryParse(inputDuration.text, out spawnData.duration)) inputDuration.text = spawnData.duration.ToString();
        if (!short.TryParse(inputRepeatCount.text, out spawnData.repeatCount)) inputRepeatCount.text = spawnData.repeatCount.ToString();
        if (!short.TryParse(inputEnemyCount.text, out spawnData.enemyCount)) inputEnemyCount.text = spawnData.enemyCount.ToString();
        stageMakerManager.UpdateInfo();
    }

    public void DeleteSpawnData()
    {
        print("DeleteSpawnData");
        GameObject.Destroy(this.gameObject);
        stageMakerManager.UpdateInfo();
    }
}
