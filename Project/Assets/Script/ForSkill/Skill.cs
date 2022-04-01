using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{

    public string Skillname;
    public int SkillDamage;
    public int SkillRange;
    public bool CanUseSkill ;
    public float SkillCoolTime;
    public float CurrentCoolTime;

    public virtual void UseSkill()
    {
        Debug.Log("Skill 스크립트의 UseSkill");
    }
}
