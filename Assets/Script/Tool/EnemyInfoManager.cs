using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfoManager : MonoBehaviour
{
    [SerializeField] GameObject preFab;
    [SerializeField] GameObject content;

    private void Awake()
    {
    }
    public void AddData()
    {
        Instantiate(preFab, content.transform).name = "new data";
    }

    public void LoadData()
    {
        EnemyInformation.Instance.LoadInformation();

        for (int i = 0; i < content.transform.childCount; i++)
            GameObject.Destroy(content.transform.GetChild(i).gameObject);

        foreach (Dictionary<int, EnemyInfo> iter1 in EnemyInformation.info.Values)
        {
            foreach (EnemyInfo iter2 in iter1.Values)
               Instantiate(preFab, content.transform).GetComponent<EnemyInfoMaker>().SetData(iter2);
        }
        UpdateDataName();
    }

    public void SaveData()
    {
        EnemyInformation.info.Clear();

        foreach (EnemyInfoMaker maker in content.transform.GetComponentsInChildren<EnemyInfoMaker>())
        {
            print("maker " + maker.name);
            EnemyInformation.Instance.SetEnemyInfo(maker.GetData());
        }

        EnemyInformation.Instance.SaveInformation();
    }

    public void UpdateDataName()
    {
        foreach (EnemyInfoMaker maker in content.transform.GetComponentsInChildren<EnemyInfoMaker>())
        {
            maker.gameObject.name = maker.GetData().enemyID + "_Lv." + maker.GetData().level;
        }
    }
}
