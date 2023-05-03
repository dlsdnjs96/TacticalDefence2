using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class EnemyInfo {
    public int enemyID;
    public int level;
    public int exp;
    public int gold;

    public EnemyInfo()
    {
        enemyID = 0;
        level = 0;
        exp = 0;
        gold = 0;
    }
}
public partial class Enemy : Entity
{
    EnemyInfo enemyInfo;

    public void Init(EnemyInfo _enemyInfo)
    {
        if (_enemyInfo == null) { EntityPool.Instance.Return(this, entityID);return; }
        enemyInfo = _enemyInfo;
        entityID = enemyInfo.enemyID;
        transform.position = spawner.transform.position + (Vector3.up * Random.Range(-1f, 1f));

        stat = StatInformation.info[enemyInfo.enemyID].baseStat +
            (StatInformation.info[enemyInfo.enemyID].bonusStat * enemyInfo.level);

        hp = stat.maxHp;
        state = EnemyState.WALK;
        isAlive = true;
        FindTarget();
        StopCoroutine(CoAttack(stat));
    }


}
