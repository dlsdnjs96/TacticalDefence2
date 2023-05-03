//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;



//public class EnemyPool : MonoBehaviour
//{

//    Dictionary<int, Enemy> prefabs; // 인스펙터에서 초기화
//    Dictionary<int, Queue<Enemy>> pools;

//    public static EnemyPool instance;


//    private void Awake()
//    {
//        if (instance == null)
//        {
//            instance = this;
//        }
//        else if (instance != this)
//        {
//            Destroy(this.gameObject);
//        }
//        LoadEnemyPrefabs();
//        //Enemy.gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
//    }

//    private void LoadEnemyPrefabs()
//    {
//        pools = new Dictionary<int, Queue<Enemy>>();
//        prefabs = new Dictionary<int, Enemy>();

//        foreach (Enemy it in Resources.LoadAll<Enemy>("Enemy"))
//        {
//            pools[int.Parse(it.gameObject.name)] = new Queue<Enemy>();
//            prefabs[int.Parse(it.gameObject.name)] = it;
//        }
//    }

//    public Enemy Create(int _enemyID)
//    {
//        Enemy newObj = Instantiate(prefabs[_enemyID], transform).GetComponent<Enemy>();
//        newObj.InitID(_enemyID);
//        newObj.gameObject.SetActive(false);
//        return newObj;
//    }



//    public Enemy Get(int _enemyID)
//    {
//        if (pools[_enemyID] == null) return null;

//        if (pools[_enemyID].Count > 0)
//        {
//            var obj = pools[_enemyID].Dequeue();
//            obj.gameObject.SetActive(true);
//            return obj;
//        }
//        else
//        {
//            var newObj = Create(_enemyID);
//            newObj.gameObject.SetActive(true);
//            return newObj;
//        }
//    }

//    public void Set(int _enemyID, int _count)
//    {
//        while (pools[_enemyID].Count < _count)
//            pools[_enemyID].Enqueue(Create(_enemyID));
//    }

//    public void Return(Enemy obj, int _enemyID)
//    {
//        obj.gameObject.SetActive(false);
//        pools[_enemyID].Enqueue(obj);
//    }
//    public void Clear(int _enemyID)
//    {
//        foreach (Enemy enemy in pools[_enemyID])
//            enemy.gameObject.SetActive(false);
//    }

//    public void ClearAll()
//    {
//        foreach (Enemy obj in transform.GetComponentsInChildren<Enemy>())
//        {
//            if (obj.gameObject.activeSelf)
//                Return(obj, obj.enemyID);
//        }
//    }

//}
