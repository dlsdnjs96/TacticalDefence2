using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Hero : Entity
{
    [SerializeField] GameObject[] skillPrefabs;
    SkillBase[] skills;
    
    private void InitSkillObjects()
    {
        skills = new SkillBase[skillPrefabs.Length];

        for (int i = 0; i < skillPrefabs.Length; i++)
            skills[i] = Instantiate(skillPrefabs[i], transform).GetComponent<SkillBase>();
    }
    private void ResetSkills()
    {
        foreach (SkillBase baseSkill in skills)
        {
            if (baseSkill != null)
                baseSkill.ResetSkill();
        }
    }
    private bool AutoActiveSkill()
    {
        for (int i = 0; i < skills.Length; i++)
        {
            if (IsActivable(i)) {
                ActiveSkill(i);
                return true;
            }
        }
        return false;
    }
    public bool IsActivable(int _skillNum)
    {
        return skills[_skillNum].IsActivable();
    }
    public void ActiveSkill(int _skillNum)
    {
        animator.SetTrigger("Skill_NORMAL");
        isAttacking = true;
        skills[_skillNum].ActiveSkill();
    }
    public void CancelActivation()
    {
        isAttacking = false;
    }
}
