using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Hero : Entity
{
    private bool isAttacking;

    public bool IsAttackable()
    {
        if (!target || !target.isAlive || !Util.WithinRange(transform.position, target.transform.position, stat.atkRange) || !isAlive)
            return false;
        return true;
    }
    public void BeginAttack()
    {
        animator.SetTrigger("Attack_" + jobType.ToString());
        isAttacking = true;
    }
    public void CancelAttack()
    {
        isAttacking = false;
        animator.SetTrigger("IDLE");
    }
    public void DeliverAttack()
    {
        if (!IsAttackable()) return;

        attackDelegate(target.GetComponent<Entity>(), GetComponent<Entity>());
    }
    public override void ApplyAttack(DamageData _damageData, Entity _attacker)
    {
        base.ApplyAttack(_damageData, _attacker);


        if (hp <= 0f && isAlive)
        {
            hp = 0f;
            isAlive = false;
            gameManager.DeadHero();
            ChangeState(HeroState.DEAD);
        }
    }

}
