using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum StageState { READY, PLAYING, PAUSE, RESULT }

public partial class StageManager : MonoBehaviour
{
    private StageState state;
    [SerializeField]
    private GameManager gameManager;


    void Start()
    {
        playTimeScale = 1f;
        currentStage = 1;

        //Test();
        //SaveData();
        LoadData();

    }

    private void SetAllReady()
    {
        System.GC.Collect();
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Hero"))
            obj.GetComponent<Hero>().SetReady();
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Enemy"))
            obj.GetComponent<Enemy>().SetReady();
    }

    private void Defeat()
    {
        SetAllReady();
        gameManager.OpenPlayWindow();
        gameManager.OpenDefeatWindow();
    }
    private void Victory()
    {
        SetAllReady();
        gameManager.OpenPlayWindow();
        gameManager.OpenVictoryWindow();
        //ShowNotice("Stage Clear", 3f);
    }

    public void ResetStage()
    {
        gameManager.ResetHerosInField();
        gameManager.ClearAllEnemy();
        ResetReward();
    }

    public void DeadHero()
    {
        if (!gameManager.HasAnyAliveHeroInField())
            Defeat();
    }
    public void KilledEnemy(int _enemyID)
    {
        killCount++;
        if (killCount >= spawnData[currentStage].enemyCount)
            Victory();
    }

    IEnumerator CoSpawnEnemy(SpawnData _enemySpawnData)
    {
        yield return new WaitForSeconds(_enemySpawnData.spawnTime);

        for (int i = 0; i < _enemySpawnData.repeatCount; i++)
        {
            // Spawn
            for (int j = 0;j < _enemySpawnData.enemyCount;j++)
                (EntityPool.instance.Get(_enemySpawnData.enemyId) as Enemy).Init(EnemyInformation.Instance.GetEnemyInfo(_enemySpawnData.enemyId, _enemySpawnData.enemyLevel));
            yield return new WaitForSeconds(_enemySpawnData.duration);
        }
    }
}
