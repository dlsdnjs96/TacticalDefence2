//using System;
using UnityEngine;
using System.Collections;
using System;

public static class Util
{

    public static bool WithinRange(Vector3 _from, Vector3 _to, float _dis)
    {
        if (Mathf.Pow(_from.x - _to.x, 2f) + Mathf.Pow(_from.y - _to.y, 2f) < Mathf.Pow(_dis, 2f))
            return true;
        return false;
    }
    public static Entity FindNearestTarget(Vector3 _from, string _tag)
    {
        float minDis = float.MaxValue, temp;
        Entity nearestTarget = null;

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag(_tag))
        {
            if (obj.GetComponent<Entity>().isAlive)
            {
                temp = Vector3.Distance(_from, obj.transform.position);
                if (temp < minDis)
                {
                    nearestTarget = obj.GetComponent<Entity>();
                    minDis = Vector3.Distance(_from, obj.transform.position);
                }
            }
        }
        return nearestTarget;
    }
    public static Entity FindTarget(Vector3 _from, float _dis, string _tag)
    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag(_tag))
        {
            if (obj.GetComponent<Entity>().isAlive && WithinRange(_from, obj.transform.position, _dis))
            {
                return obj.GetComponent<Entity>();
            }
        }
        return null;
    }
    
    
    public static void Swap<T>(ref T lhs, ref T rhs)
    {
        T temp;
        temp = lhs;
        lhs = rhs;
        rhs = temp;
    }

    public static bool IsSuccess(float _prob)
    {
        return UnityEngine.Random.Range(0.0001f, 100f) <= _prob;
    }

    public static float GetEluerDirection(Vector3 _dir)
    {
        return Mathf.Atan2(_dir.y, _dir.x) * Mathf.Rad2Deg;
    }
    public static DateTime TimeStampToDateTime(long value)
    {
        DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        dt = dt.AddSeconds(value).ToLocalTime();
        return dt;
    }
}
