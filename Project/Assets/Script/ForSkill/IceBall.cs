using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBall : Skill
{
    public GameObject SkillEffectPrefab;
    void Awake()
    {
        Initialization();
    }

    private void Initialization()
    {
        this.Skillname = "FireBall";
    }
    public override void UseSkill()
    {
        this.SkillDamage = PlayerInfo.instance.MagicDamage;;
        Vector3 PlayerPos = DummyController.instance.transform.position;
        Vector3 EffectPos = PlayerPos + new Vector3(1f, 1f, 0);
        Instantiate(SkillEffectPrefab, EffectPos, ForDir.instance.transform.rotation);
        // StartCoroutine("CoolTime");
        // CurrentCoolTime = SkillCoolTime;
        // StartCoroutine("CooltimeCounter");
        // CanUseSkill = false;
        return;
    }
}
