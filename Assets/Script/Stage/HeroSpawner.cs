using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSpawner : MonoBehaviour
{
    public GameManager gameManager;

    Transform[] slots;

    [SerializeField]
    private Dictionary<int, Hero> heroList;

    private void Awake()
    {
        slots = GetComponentsInChildren<Transform>();
        LoadHeros();
    }

    private void LoadHeros()
    {
        heroList = new Dictionary<int, Hero>();
        heroList[10000] = Resources.Load<Hero>("Hero/10000");
        heroList[20000] = Resources.Load<Hero>("Hero/20000");
        heroList[21000] = Resources.Load<Hero>("Hero/21000");
        heroList[30000] = Resources.Load<Hero>("Hero/30000");
    }
   

    private void ClearSpawnedHero()
    {
        for (int i = 1; i <= 12; i++)
        {
            Transform[] childrenList = slots[i].GetComponentsInChildren<Transform>();
            for (int j = 1;j < childrenList.Length;j++)
                    GameObject.Destroy(childrenList[j].gameObject);
            
        }
    }
    public void InsertDeck()
    {
        ClearSpawnedHero();
        for (int i = 1;i <= 12;i++)
        {
            if (gameManager.GetCurrentDeck().slots[i - 1] != "") { 
                HeroInfo heroInfo = Database.Instance.GetHeroInfoByUID(gameManager.GetCurrentDeck().slots[i - 1]);
                Hero hero = Instantiate(heroList[heroInfo.heroID], slots[i].transform);
                hero.ApplyHeroInfo(heroInfo);
                hero.SetReady();
            }
        }
    }

    public int GetAliveHeroCount()
    {
        int count = 0;
        for (int i = 1; i <= 12; i++)
        {
            if (gameManager.GetCurrentDeck().slots[i - 1] != "")
            {
                if (slots[i].transform.GetChild(0) != null && slots[i].transform.GetChild(0).GetComponent<Hero>().isAlive)
                    count++;
            }
        }
        return count;
    }

    public bool HasAnyAliveHero()
    {
        for (int i = 0; i < 12; i++)
        {
            if (gameManager.GetCurrentDeck().slots[i] != "" && slots[i + 1].transform.GetChild(0).GetComponent<Hero>().isAlive)
                return true;
        }
        return false;
    }

    public bool HasAnyHero()
    {
        for (int i = 0; i < 12; i++)
        {
            if (gameManager.GetCurrentDeck().slots[i] != "")
                return true;
        }
        return false;
    }

    public void ResetHeros()
    {
        for (int i = 1; i <= 12; i++)
        {
            if (gameManager.GetCurrentDeck().slots[i - 1] != ""
                && slots[i].transform.GetChild(0) != null
                && slots[i].transform.GetChild(0).GetComponent<Hero>() != null)
                slots[i].transform.GetChild(0).GetComponent<Hero>().ResetHero();
        }
    }
}
