using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyState { IDLE = 0, WALK = 1, ATTACK = 2, DEAD = 3, READY = 4 }
public partial class Enemy : Entity
{
    [SerializeField] private EnemyState  state;


    public static GameObject spawner;
    public static GameManager gameManager;


    protected override void Awake()
    {
        base.Awake();
        hurt = transform.Find("HurtEffect").GetComponent<ParticleSystem>();
        stat = new Stat();
    }

    void OnEnable()
    {
        status = StatusPool.instance.GetObject();
        status.InitializeHP(this);
    }

    private void OnDisable()
    {
        if (status != null) StatusPool.instance.ReturnObject(status);
    }

    void Update()
    {
        switch (state)
        {
            case EnemyState.IDLE:
                Idle();
                break;
            case EnemyState.WALK:
                Walk();
                break;
            case EnemyState.ATTACK:
                Attack();
                break;
            case EnemyState.DEAD:
                Dead();
                break;
            case EnemyState.READY:
                Ready();
                break;
        }
    }

    void Idle()
    {
        FindTarget();

        if (target != null) state = EnemyState.WALK;
    }

    void Walk()
    {
        if (target == null) { state = EnemyState.IDLE; return; }

        if (!target.isAlive)
        {
            FindTarget();
            return;
        }
        else if (CanAttack())
        {
            state = EnemyState.ATTACK;
            StartCoroutine(CoAttack(stat));
        }
        transform.position += (target.transform.position - transform.position).normalized * 3f * Time.deltaTime;
    }

    void Attack()
    {
        if (target == null) { state = EnemyState.IDLE; StopCoroutine(CoAttack(stat)); return; }

        if (!target.isAlive)
        {
            FindTarget();
            return;
        }
        else if (!CanAttack()) {
            FindTarget();
            state = EnemyState.WALK;
            StopCoroutine(CoAttack(stat));
            return;
        }


    }

    void Dead()
    {

    }
    void Ready()
    {

    }
    public void DeliverAttack()
    {
        if (!target) return;

        attackDelegate(target.GetComponent<Entity>(), GetComponent<Entity>());
    }

    private void FindTarget()
    {
        target = Util.FindNearestTarget(transform.position, "Hero");
    }

    private bool CanAttack()
    {
        return Util.WithinRange(target.transform.position, transform.position, stat.atkRange);
    }

    public void SetReady()
    {
        state = EnemyState.READY;
        StopCoroutine(CoAttack(stat));
    }

    public override void ApplyAttack(DamageData _damageData, Entity _attacker)
    {
        base.ApplyAttack(_damageData, _attacker);

        // ««∞› ¿Ã∆Â∆Æ
        hurt.Play();

        if (hp <= 0f && isAlive)
        {
            hp = 0f;
            isAlive = false;
            state = EnemyState.DEAD;
            StopCoroutine(CoAttack(stat));
            OnDead();
            EntityPool.instance.Return(this, entityID);
        }
    }

    IEnumerator CoAttack(Stat _attackerStat)
    {
        yield return new WaitForSeconds(1f / stat.atkSpeed);
        DeliverAttack();
        if (state == EnemyState.ATTACK) StartCoroutine(CoAttack(_attackerStat));
    }
}
