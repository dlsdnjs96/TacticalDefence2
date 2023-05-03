using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProjectileType { MagicBall, Dagger, Arrow, FireBall, GreenBall, ElectricBall, Shuriken }

public class Projectile : MonoBehaviour
{
    private Entity target;
    private Vector3 from;
    private Entity attacker;

    private Animator animator;

    private float passedTime;
    private float arrivedTime;

    delegate void ProjectileUpdate();
    private ProjectileUpdate projectileUpdate;

    public static DamageManager damageManager;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // 타겟이 죽을경우 투사체 반납
        if (target == null || !target.isAlive)
        {
            ProjectilePool.instance.ReturnObject(this);
            return;
        }

        // 날아온 시간 측정
        passedTime += Time.deltaTime;

        // 도착시간을 지났을경우 피격
        if (passedTime >= arrivedTime)
        {
            damageManager.ApplyDamage(attacker, target);
            ProjectilePool.instance.ReturnObject(this);
            return;
        }

        // (지난시간 / 소요시간)으로 투사체 위치 계산
        transform.position = Vector3.Lerp(from, target.transform.position, passedTime / arrivedTime);
        // 투사체 종류에 따라 회전(제자리 회전 or 타겟방향으로 회전)
        projectileUpdate();
    }

    public void Initialize(Entity _target, Entity _attacker)
    {
        // 공격 유닛 & 타겟 유닛 설정
        target = _target;
        attacker = _attacker;
        from = _attacker.shooter.transform.position;

        // 투사체 날아가는 방식 지정
        InitProjectileType(_attacker.projectileType);
        // 투사체 이미지 변경
        animator.SetTrigger(_attacker.projectileType.ToString());

        // 거리에 따라 날아가는 시간 설정
        arrivedTime = Vector3.Distance(from, _target.transform.position) / 10f; 
        passedTime = 0f;
    }

    private void InitProjectileType(ProjectileType _projectileType)
    {
        switch(_projectileType)
        {
            case ProjectileType.MagicBall:
            case ProjectileType.FireBall:
            case ProjectileType.ElectricBall:
            case ProjectileType.GreenBall:
            case ProjectileType.Dagger:
                projectileUpdate = BallUpdate;
                break;
            case ProjectileType.Arrow:
            case ProjectileType.Shuriken:
                projectileUpdate = ArrowUpdate;
                break;
            default:
                projectileUpdate = DefaultUpdate;
                break;
        }
    }

    void DefaultUpdate() { }
    void BallUpdate() { 
        transform.rotation = Quaternion.Euler(0f, 0f, -passedTime * Mathf.Rad2Deg * 20f); 
    }
    void ArrowUpdate() { 
        transform.rotation = Quaternion.Euler(0f, 0f, 
            Util.GetEluerDirection(target.transform.position - transform.position) - 90f); 
    }
}
