using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DamageManager : MonoBehaviour
{
    private void Awake()
    {
        Entity.damageManager  = this;
        Projectile.damageManager    = this;
        SkillBase.damageManager     = this;
    }

    public void ApplyDamage(Entity _attacker, Entity _victom)
    {
        StartCoroutine(CoCalculateDamage(_attacker, _victom, new SkillData()));
    }
    public void ApplyDamage(Entity _attacker, Entity _victom, SkillData _skillData)
    {
        StartCoroutine(CoCalculateDamage(_attacker, _victom, _skillData));
    }

    IEnumerator CoCalculateDamage(Entity _attacker, Entity _victom, SkillData _skillData)
    {
        DamageData damageData = new DamageData();

        for (int i = 0; i < _skillData.attackCount; i++)
        {
            // ��ų ������ ���
            damageData.damage = _skillData.damagePercent * _attacker.stat.wAtk / 100f;

            // ���� ���� ���
            if ((damageData.option & DamageOption.MAGIC_ATTACK) != 0)
            {
                damageData.damage -= damageData.damage * (_victom.stat.defensive) / (Constant.ARMOUR_CONSTANT + _victom.stat.defensive);
            }
            // ���� ���� ���
            else
            {
                damageData.damage += damageData.damage * Mathf.Clamp((float)(_attacker.level - _victom.level) / 100f, -1f, 1f);
            }

            // ġ��Ÿ ���
            if (Util.IsSuccess(_attacker.stat.criProb))
            {
                damageData.damage += damageData.damage * (50f + _attacker.stat.criDamage);
                damageData.option |= DamageOption.CRITICAL;
            }

            // ������ ����
            damageData.damage *= Random.Range(0.7f, 1f);

            // �Ҽ��� ����
            damageData.damage = Mathf.Round(damageData.damage);



            _victom.ApplyAttack(damageData, _attacker);
            DamageTextPool.instance.GetObject().Initialize(damageData, _victom.statusObj.transform.position);
            yield return new WaitForSeconds(0.5f / _skillData.attackCount);
        }

    }
}



[System.Flags]
public enum DamageOption : int{
    MAGIC_ATTACK    = 1 << 0, // ���� or ����
    CRITICAL        = 1 << 1, // ũ�� or �Ϲ�
    SKILL_ATTACK    = 1 << 2, // ��ų or ��Ÿ
    MULTI_TARGET    = 1 << 3, // ��Ƽ or ����
}

public struct DamageData
{
    public float damage;
    public DamageOption option;
}

// bit mask : physic/magic, cri/non-cri, normal/skill, 

[System.Serializable]
public class SkillData
{
    public float damagePercent;
    public int attackCount;
    public DamageOption option;

    public SkillData() { damagePercent = 100f; attackCount = 1; option = 0; }
}