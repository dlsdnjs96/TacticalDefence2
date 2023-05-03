using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillBase : MonoBehaviour
{
    [SerializeField] public string skillName;
    [SerializeField] public float cooltime;
    [SerializeField] public float motionDuration;
    [SerializeField] public float skillDuration;
    [SerializeField] public SkillData skillData;
    public float expiredTime { get; protected set; }
    public bool isAutoUsing { get; protected set; }
    public Hero activator { get; protected set; }

    public virtual bool IsActivable() { return expiredTime <= Time.time; }
    public abstract void ActiveSkill();
    public abstract void DeActiveSkill();


    protected static int enemyLayerMask;

    public static DamageManager damageManager;


    public void ConsumeCooltime() { 
        expiredTime = Time.time + (cooltime * (100f - activator.stat.coolTimeReduction) / 100f); 
    }
    public virtual void ResetSkill() { expiredTime = Time.time; }

    //public abstract void OnDamaged();
}
