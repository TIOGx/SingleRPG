using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{

    public string Skillname;
    public int SkillDamage;
    public int SkillRange;

    public virtual void UseSkill()
    {
        Debug.Log("Skill ��ũ��Ʈ�� UseSkill");
    }
}
