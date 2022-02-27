using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : Skill
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
        Debug.Log("FireBall~");
        Debug.Log("자식 스크립트의 UseSkill");
        Vector3 PlayerPos = DummyController.instance.transform.position; // 플레이어 좌표
        Collider[] hitCol = Physics.OverlapSphere(PlayerPos, SkillRange); // 공격 범위 내 콜라이더 식별
        for (int i = 0; i < hitCol.Length; i++)
        {
            Debug.Log(hitCol[i].gameObject.name);
            if (hitCol[i].gameObject.CompareTag("Enemy"))
            {
                // 스킬 이펙트
                // Instantiate(SkillEffectPrefab, hitCol[i].gameObject.transform);
                hitCol[i].gameObject.GetComponent<EnemyController>().TakeDamage(SkillDamage);
            }
            else{
                continue;
            }
        }
        return;
    }
}
