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
        this.CanUseSkill = false;
        this.SkillCoolTime = 5;
    }
    public override void UseSkill()
    {
        Debug.Log("FireBall~");
        Debug.Log("???? ?????????? UseSkill");
        Vector3 PlayerPos = DummyController.instance.transform.position; // ???????? ????
        Collider[] hitCol = Physics.OverlapSphere(PlayerPos, SkillRange); // ???? ???? ?? ???????? ????
        for (int i = 0; i < hitCol.Length; i++)
        {
            Debug.Log(hitCol[i].gameObject.name);
            if (hitCol[i].gameObject.CompareTag("Enemy"))
            {
                // ��ų ����Ʈ
                hitCol[i].gameObject.GetComponent<EnemyController>().TakeDamage(SkillDamage);
                Vector3 EffectPos = hitCol[i].gameObject.transform.position + new Vector3(0, 2f, 0);
                Instantiate(SkillEffectPrefab, EffectPos, Quaternion.identity);
            }
            else{
                continue;
            }
        }
        StartCoroutine("CoolTime");
        CurrentCoolTime = SkillCoolTime;
        StartCoroutine("CooltimeCounter");
        // CanUseSkill = false;
        return;
    }
    IEnumerator CoolTime()
    {
        while (CurrentCoolTime > 0)
        {
            yield return null;
        }
        CanUseSkill = true;
        yield break;
    }
    IEnumerator CooltimeCounter()
    {
        while(CurrentCoolTime > 0)
        {
            yield return new WaitForSeconds(1.0f);
            CurrentCoolTime -= 1f;

        }
        yield break;
    }
}
