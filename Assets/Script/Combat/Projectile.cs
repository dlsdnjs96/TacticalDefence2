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
        // Ÿ���� ������� ����ü �ݳ�
        if (target == null || !target.isAlive)
        {
            ProjectilePool.instance.ReturnObject(this);
            return;
        }

        // ���ƿ� �ð� ����
        passedTime += Time.deltaTime;

        // �����ð��� ��������� �ǰ�
        if (passedTime >= arrivedTime)
        {
            damageManager.ApplyDamage(attacker, target);
            ProjectilePool.instance.ReturnObject(this);
            return;
        }

        // (�����ð� / �ҿ�ð�)���� ����ü ��ġ ���
        transform.position = Vector3.Lerp(from, target.transform.position, passedTime / arrivedTime);
        // ����ü ������ ���� ȸ��(���ڸ� ȸ�� or Ÿ�ٹ������� ȸ��)
        projectileUpdate();
    }

    public void Initialize(Entity _target, Entity _attacker)
    {
        // ���� ���� & Ÿ�� ���� ����
        target = _target;
        attacker = _attacker;
        from = _attacker.shooter.transform.position;

        // ����ü ���ư��� ��� ����
        InitProjectileType(_attacker.projectileType);
        // ����ü �̹��� ����
        animator.SetTrigger(_attacker.projectileType.ToString());

        // �Ÿ��� ���� ���ư��� �ð� ����
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
