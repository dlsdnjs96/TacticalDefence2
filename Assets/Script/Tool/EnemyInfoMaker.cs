using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyInfoMaker : MonoBehaviour
{
    [SerializeField] public EnemyInfo enemyInfo;
    [SerializeField] public Transform enemyImage;
    [SerializeField] public TMP_InputField id;
    [SerializeField] public TMP_InputField level;
    [SerializeField] public TMP_InputField exp;
    [SerializeField] public TMP_InputField gold;

    private void Awake()
    {
        enemyInfo = new EnemyInfo();
    }

    public EnemyInfo GetData()
    {
        return enemyInfo;
    }
    public void SetData(EnemyInfo _enemyInfo)
    {
        enemyInfo = _enemyInfo;
        id.text = enemyInfo.enemyID.ToString();
        level.text = enemyInfo.level.ToString();
        gold.text = enemyInfo.gold.ToString();
        exp.text = enemyInfo.exp.ToString();
        UpdateData();
        return;
    }

    public void UpdateData()
    {
        Enemy enemy = EntityPool.Instance.Get(1000000) as Enemy;
        enemy.gameObject.transform.SetParent(enemyImage.transform);
        enemy.gameObject.transform.localPosition = Vector3.zero;

        int.TryParse(id.text, out enemyInfo.enemyID);
        int.TryParse(level.text, out enemyInfo.level);
        int.TryParse(gold.text, out enemyInfo.gold);
        int.TryParse(exp.text, out enemyInfo.exp);
    }
}
