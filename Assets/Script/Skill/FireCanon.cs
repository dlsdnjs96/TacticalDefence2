using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCanon : SkillBase
{
    [SerializeField]
    private float range;
    [SerializeField]
    private GameObject[] balls;
    private Entity[] targets;
    private int targetCount = 0;

    private void Awake()
    {
        activator = GetComponentInParent<Hero>();
        enemyLayerMask = LayerMask.GetMask("Enemy");
        SetBallsActive(false);
    }

    public override bool IsActivable()
    {
        if (!base.IsActivable() || !AnyEnemyInRange())
            return false;
        return true;
    }

    private bool AnyEnemyInRange()
    {
        foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (Util.WithinRange(activator.transform.position, enemy.transform.position, range))
                return true;
        }
        return false;
    }

    private void SetBallsActive(bool _active)
    {
        for (int i = 0; i < balls.Length; i++)
            balls[i].SetActive(_active);
    }

    private void ActiveBalls(bool _active)
    {
        for (int i = 0; i < targetCount; i++)
            balls[i].SetActive(_active);
    }

    private void FindTargets()
    {
        targetCount = 0;
        targets = new Entity[balls.Length];

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (Util.WithinRange(activator.transform.position, enemy.transform.position, range))
            {
                targets[targetCount] = enemy.GetComponent<Entity>();
                targetCount++;
            }
            if (targetCount >= balls.Length)
                break;
        }
    }

    public override void ActiveSkill()
    {
        ConsumeCooltime();
        SetBallsActive(false);
        FindTargets();
        SetBallsDirection();

        StartCoroutine(CoDamaging());
    }
    public override void DeActiveSkill()
    {
        StopCoroutine(CoDamaging());
    }


    private void SetBallsDirection()
    {
        for (int i = 0; i < targetCount; i++)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, Util.GetEluerDirection(targets[i].transform.position - activator.shooter.transform.position) + 180f);
        }
    }
    IEnumerator CoDamaging()
    {
        yield return new WaitForSeconds(motionDuration);

        ActiveBalls(true);

        float passedTime = 0f;
        while (passedTime < skillDuration)
        {
            passedTime += Time.deltaTime;
            for (int i = 0;i < targetCount;i++)
            {
                if (!targets[i].isAlive) balls[i].gameObject.SetActive(false);

                balls[i].transform.position = Vector3.Lerp(activator.shooter.transform.position, targets[i].transform.position, passedTime / skillDuration);
            }
            yield return null;
        }
        for (int i = 0; i < targetCount; i++)
            damageManager.ApplyDamage(activator, targets[i], skillData);

        SetBallsActive(false);
    }
}
