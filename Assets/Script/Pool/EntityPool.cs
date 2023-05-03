using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EntityPool : MonoBehaviour
{

    Dictionary<int, Entity> prefabs;
    Dictionary<int, Queue<Entity>> pools;

    public static EntityPool instance;
    public static EntityPool Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj;
                obj = GameObject.Find("EntityPool");
                if (obj == null)
                {
                    obj = new GameObject("EntityPool");
                    instance = obj.AddComponent<EntityPool>();
                }
                else
                {
                    instance = obj.GetComponent<EntityPool>();
                }
            }
            return instance;
        }
    }


    private void Awake()
    {
        //DontDestroyOnLoad(this);
        if (instance == null)
        {
            instance = this;
        }
        LoadEntities();
        //Enemy.gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void LoadEntities()
    {
        pools = new Dictionary<int, Queue<Entity>>();
        prefabs = new Dictionary<int, Entity>();

        foreach (Entity it in Resources.LoadAll<Entity>("Hero/"))
        {
            pools[int.Parse(it.gameObject.name)] = new Queue<Entity>();
            prefabs[int.Parse(it.gameObject.name)] = it;
        }
        foreach (Entity it in Resources.LoadAll<Entity>("Enemy"))
        {
            pools[int.Parse(it.gameObject.name)] = new Queue<Entity>();
            prefabs[int.Parse(it.gameObject.name)] = it;
        }
    }

    public Entity Create(int _objID)
    {
        Entity newObj = Instantiate(prefabs[_objID], transform).GetComponent<Entity>();
        newObj.gameObject.SetActive(false);
        return newObj;
    }



    public Entity Get(int _entityID)
    {
        if (pools[_entityID] == null) return null;

        if (pools[_entityID].Count > 0)
        {
            var obj = pools[_entityID].Dequeue();
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            var newObj = Create(_entityID);
            newObj.gameObject.SetActive(true);
            return newObj;
        }
    }

    public void Set(int _entityID, int _count)
    {
        while (pools[_entityID].Count < _count)
            pools[_entityID].Enqueue(Create(_entityID));
    }

    public void Return(Entity obj, int _entityID)
    {
        obj.gameObject.SetActive(false);
        pools[_entityID].Enqueue(obj);
    }
    public void Clear(int _entityID)
    {
        foreach (Entity enemy in pools[_entityID])
            enemy.gameObject.SetActive(false);
    }

    public void ReturnAll()
    {
        foreach (Entity obj in transform.GetComponentsInChildren<Entity>())
        {
            if (obj.gameObject.activeSelf)
                Return(obj, obj.entityID);
        }
    }

    public void ReturnWithTag(string _tag)
    {
        foreach (Entity obj in transform.GetComponentsInChildren<Entity>())
        {
            if (obj.gameObject.activeSelf && obj.CompareTag(_tag))
                Return(obj, obj.entityID);
        }
    }
}
