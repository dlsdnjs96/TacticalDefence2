using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerShelling : SkillBase
{
    [SerializeField] private float lazerRange;
    [SerializeField] private float postDelay;
    [SerializeField] GameObject beam;
    [SerializeField] GameObject ball;
    [SerializeField] GameObject ring;
    [SerializeField] GameObject rebound;

    [SerializeField] SpriteRenderer beamSprite;
    [SerializeField] SpriteRenderer ballSprite;

    private Entity target;
    private int targetCount = 0;

    private void Awake()
    {
        activator = GetComponentInParent<Hero>();
        enemyLayerMask = LayerMask.GetMask("Enemy");
    }



    private bool FindFarthestEnemy()
    {
        float maxDis = float.MinValue;
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            float dis = Vector3.Distance(ball.transform.position, enemy.transform.position);
            if (dis > maxDis)
            {
                maxDis = dis;
                target = enemy.GetComponent<Enemy>();
            }
        }
        if (maxDis == float.MinValue) return false;
        return true;
    }

    public override bool IsActivable()
    {
        if (!base.IsActivable() || !FindFarthestEnemy())
            return false;
        return true;
    }

    public override void ActiveSkill()
    {
        ConsumeCooltime();
        SetBeamDirection();

        StartCoroutine(CoDamaging());
    }
    public override void DeActiveSkill()
    {
        StopCoroutine(CoDamaging());
    }


    private void SetBeamDirection()
    {
        beam.transform.rotation = 
            Quaternion.Euler(0f, 0f, Util.GetEluerDirection(target.transform.position - ball.transform.position) + 90f );

        beam.transform.position = ball.transform.position + (Vector3.Normalize(target.transform.position - ball.transform.position) * 12f);
        //beam.transform.position = (target.transform.position + ball.transform.position) / 2f;

    }
    IEnumerator CoDamaging()
    {
        ball.gameObject.SetActive(true);
        yield return new WaitForSeconds(motionDuration);

        beam.gameObject.SetActive(true);
        ball.gameObject.SetActive(true);
        ring.gameObject.SetActive(true);
        rebound.gameObject.SetActive(true);

        float passedTime = 0f;
        while (passedTime < skillDuration)
        {
            passedTime += Time.deltaTime;

            beam.transform.localScale = (Vector3.right * (passedTime / skillDuration) * 3.0f) + (Vector3.up * 30f);
            ball.transform.localScale = (Vector3.one * 1.0f) + (Vector3.one * (passedTime / skillDuration) * 0.0f);
            rebound.transform.localScale = (Vector3.one) + (Vector3.one * (passedTime / skillDuration) * 2.0f);
            rebound.transform.localPosition = (Vector3.right * (passedTime / skillDuration) * 1.3f) + (Vector3.up * 0.45f);
            ring.transform.localScale = Vector3.one * (passedTime / skillDuration) * 4f;
            //ring.transform.localPosition = (Vector3.right * (passedTime / skillDuration) * 0.5f) + (Vector3.up * 0.45f);

            ballSprite.color = (Color.white * 0.6f) + (Color.white * (passedTime / skillDuration) * 0.4f);
            beamSprite.color = Color.white * (passedTime / skillDuration);
            yield return null;
        }

        Collider2D[] enemies = Physics2D.OverlapBoxAll(beam.transform.position, beam.transform.lossyScale, beam.transform.eulerAngles.z, enemyLayerMask);

        foreach (Collider2D enemy in enemies)
        {
            damageManager.ApplyDamage(activator, enemy.GetComponent<Entity>(), skillData);
        }

        ring.gameObject.SetActive(false);
        rebound.gameObject.SetActive(false);

        passedTime = 0f;
        while (passedTime < postDelay)
        {
            passedTime += Time.deltaTime;

            ballSprite.color = Color.white * ((postDelay - passedTime) / postDelay);
            beamSprite.color = Color.white * ((postDelay - passedTime) / postDelay);
            yield return null;
        }

        beam.gameObject.SetActive(false);
        ball.gameObject.SetActive(false);

    }
}
