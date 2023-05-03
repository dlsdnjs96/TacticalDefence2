using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatMakerManager : MonoBehaviour
{
    [SerializeField] GameObject preFab;

    private void Awake()
    {
    }
    public void AddHeroData()
    {
        Instantiate(preFab, transform).name = "new data";
    }

    public void LoadHeroData()
    {
        print(StatInformation.Instance);
        StatInformation.Instance.LoadStatInformation();

        foreach (EntityData entityData in StatInformation.info.Values)
        {
            Instantiate(preFab, transform).GetComponent<EntityStatMaker>().SetEntityData(entityData);
        }
        UpdateDataName();
    }

    public void SaveHeroData()
    {
        print(StatInformation.Instance);
        StatInformation.info.Clear();

        foreach(EntityStatMaker heroStatMaker in transform.GetComponentsInChildren<EntityStatMaker>())
        {
            EntityData heroData = heroStatMaker.GetEntityData();
            StatInformation.info.Add(heroData.entityID, heroData);
        }
        StatInformation.Instance.SaveStatInformation();
    }

    public void UpdateDataName()
    {
        foreach(EntityStatMaker heroStatMaker in transform.GetComponentsInChildren<EntityStatMaker>())
        {
            heroStatMaker.gameObject.name = heroStatMaker.entityID.ToString();
        }
    }
}
