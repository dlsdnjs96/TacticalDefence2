using System.Collections.Generic;
using UnityEngine;


public class ObjectPool<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField]
    private GameObject poolingPrefab;
    [SerializeField]
    private GameObject parentObj;

    Queue<T> pooling = new Queue<T>();

    public static ObjectPool<T> instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    public void Initialize(int initCount)
    {
        for (int i = 0; i < initCount; i++)
        {
            pooling.Enqueue(CreateNewObject());
        }
    }

    public T CreateNewObject()
    {
        var newObj = Instantiate(poolingPrefab, parentObj.transform).GetComponent<T>();
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(transform);
        return newObj;
    }

    public T GetObject()
    {
        if (pooling.Count > 0)
        {
            var obj = pooling.Dequeue();
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            var newObj = CreateNewObject();
            newObj.gameObject.SetActive(true);
            return newObj;
        }
    }

    public void ReturnObject(T obj)
    {
        obj.gameObject.SetActive(false);
        pooling.Enqueue(obj);
    }
}

