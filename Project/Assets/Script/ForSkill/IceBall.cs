using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBall : Skill
{
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
        Debug.Log("IceBall~");
        Debug.Log("자식 스크립트의 UseSkill");
        Vector3 PlayerPos = DummyController.instance.transform.position; // 플레이어 좌표
        Collider[] hitCol = Physics.OverlapSphere(PlayerPos, SkillRange); // 공격 범위 내 콜라이더 식별
        for (int i = 0; i < hitCol.Length; i++)
        {
            if (hitCol[i].gameObject.CompareTag("Enemy"))
            {
                hitCol[i].gameObject.GetComponent<EnemyController>().TakeDamage(SkillDamage);
            }
            else
            {
                continue;
            }
        }
        return;
    }
}
