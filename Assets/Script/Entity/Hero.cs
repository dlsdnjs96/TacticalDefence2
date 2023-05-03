using UnityEngine;


public enum HeroState { IDLE = 0, ATTACK = 1, ACTIVATION = 2, STUN = 3, DEAD = 4, READY = 5 }

public partial class Hero : Entity
{
    [SerializeField] public HeroState state;
    private Animator animator;
    public static GameManager gameManager;


    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        isAttacking = false;
        InitSkillObjects();
        equipment = new Equipment[3];
    }
    void OnEnable()
    {
        status = StatusPool.instance.GetObject();
        status.InitializeHP(this);

        //ResetHero();
    }
    private void OnDisable()
    {
        if (status != null) StatusPool.instance.ReturnObject(status);
    }

    void Update()
    {
        switch(state)
        {
            case HeroState.IDLE:
                Idle();
                break;
            case HeroState.ATTACK:
                Attack();
                break;
            case HeroState.ACTIVATION:
                Activation();
                break;
            case HeroState.DEAD:
                Dead();
                break;
            case HeroState.READY:
                Ready();
                break;
        }
    }

    void Idle()
    {
        target = Util.FindTarget(transform.position, stat.atkRange, "Enemy");


        if (AutoActiveSkill())
        {
            ChangeState(HeroState.ACTIVATION);
            return;
        } else if (target)
        {
            BeginAttack();
            ChangeState(HeroState.ATTACK);
            return;
        }
    }
    void Attack()
    {
        if (!isAttacking || !IsAttackable())
        {
            CancelAttack();
            ChangeState(HeroState.IDLE);
            return;
        } 
    }
    void Activation()
    {
        if (!isAttacking && !AutoActiveSkill())
        {
            CancelActivation();
            ChangeState(HeroState.IDLE);
            return;
        }
    }
    void Dead()
    {
    }
    void Ready()
    {
    }

    private void ChangeState(HeroState _state)
    {
        state = _state;

        if (state == HeroState.DEAD)
            animator.SetTrigger("Dead");
    }
    public void ResetHero()
    {
        CalculateStat();
        hp = stat.maxHp;
        status.UpdateHp();
        isAlive = true;
        ChangeState(HeroState.IDLE);
        ShowAliveForm();
        ResetSkills();
    }
    public void SetReady()
    {
        transform.localPosition = Vector3.zero;
        ChangeState(HeroState.READY);
    }
    public void ShowDeadForm()
    {
        transform.Find("Root").gameObject.SetActive(false);
        transform.Find("Tomb").gameObject.SetActive(true);
    }
    public void ShowAliveForm()
    {
        transform.Find("Root").gameObject.SetActive(true);
        transform.Find("Tomb").gameObject.SetActive(false);
    }
}

public class HeroInfo
{
    public string heroUID;
    public int heroID;
    public int enhancement;
    public int level;
    public int exp;

    public HeroInfo() { }
    public HeroInfo(int _heroID) { heroID = _heroID; }
}