using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public enum JobType { WARRIOR = 1, ARCHER = 2, MAGICIAN = 3 }

public class Entity : MonoBehaviour
{
    [SerializeField] protected JobType jobType;
    [SerializeField] public ProjectileType projectileType;
    public Stat stat { protected set; get; }
    public int entityID;
    protected EnemyStatus status;


    public Entity target { protected set; get; }
    public GameObject statusObj { protected set; get; }
    public bool isAlive { protected set; get; }
    public float hp { protected set; get; }
    public float shield { protected set; get; }
    public int level { protected set; get; }
    public GameObject shooter { protected set; get; }

    protected delegate void AttackDelegate(Entity _target, Entity _attacker);
    protected AttackDelegate attackDelegate;

    public static DamageManager damageManager;

    protected virtual void Awake()
    {
        FindStatus();
        FindShooter();
        InitAttackDel();
    }
    public void InitID(int _entityID) { entityID = _entityID; }
    protected void FindShooter()
    {
        Transform[] children = GetComponentsInChildren<Transform>();

        foreach (Transform child in children)
        {
            if (child.name == "Shooter")
            {
                shooter = child.gameObject;
                return;
            }
        }
        shooter = gameObject;
        return;
    }
    protected void FindStatus()
    {
        statusObj = transform.Find("Status").gameObject;
        if (statusObj == null) statusObj = gameObject;
    }
    public virtual void ApplyAttack(DamageData _damageData, Entity _attacker)
    {
        hp -= _damageData.damage;

        // 상태바 업데이트
        if (status != null) status.UpdateHp();
    }
    protected void AttackProjectile(Entity _target, Entity _attacker) { ProjectilePool.instance.GetObject().Initialize(_target, this); }
    protected void AttackSword(Entity _target, Entity _attacker) { damageManager.ApplyDamage(this, target); }

    protected void InitAttackDel()
    {
        switch (jobType)
        {
            case JobType.WARRIOR:
                attackDelegate = new AttackDelegate(AttackSword);
                break;
            case JobType.ARCHER:
            case JobType.MAGICIAN:
                attackDelegate = new AttackDelegate(AttackProjectile);
                break;
        }
    }
}
