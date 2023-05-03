using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelugeOfFire : SkillBase
{
    [SerializeField] private float tickTime;
    [SerializeField] private Vector2 skillSize;
    private ParticleSystem particle;

    public override bool IsActivable() {
        if (!base.IsActivable() || 
            Physics2D.OverlapBoxAll(transform.position, new Vector2(8f, 5f), 0f, enemyLayerMask).Length == 0)
            return false;
        return true;
    }
    public override void ActiveSkill()
    {
        ConsumeCooltime();
        particle.Play();
        StartCoroutine(CoDamaging());
    }
    public override void DeActiveSkill()
    {
        particle.Stop();
        StopCoroutine(CoDamaging());
    }
    private void Awake()
    {
        activator = GetComponentInParent<Hero>();
        particle = GetComponent<ParticleSystem>();
        enemyLayerMask = LayerMask.GetMask("Enemy");
    }



    IEnumerator CoDamaging()
    {
        yield return new WaitForSeconds(motionDuration);

        for (float time = 0f; time < skillDuration; time += tickTime)
        {
            Collider2D[] enemies = Physics2D.OverlapBoxAll(transform.position, skillSize, 0f, enemyLayerMask);

            foreach (Collider2D enemy in enemies)
            {
                damageManager.ApplyDamage(activator, enemy.GetComponent<Entity>(), skillData);
            }
            yield return new WaitForSeconds(tickTime);
        }
        particle.Clear();
        particle.Stop();
    }
}
