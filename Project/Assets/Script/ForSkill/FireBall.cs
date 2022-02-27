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
        Debug.Log("�ڽ� ��ũ��Ʈ�� UseSkill");
        Vector3 PlayerPos = DummyController.instance.transform.position; // �÷��̾� ��ǥ
        Collider[] hitCol = Physics.OverlapSphere(PlayerPos, SkillRange); // ���� ���� �� �ݶ��̴� �ĺ�
        for (int i = 0; i < hitCol.Length; i++)
        {
            Debug.Log(hitCol[i].gameObject.name);
            if (hitCol[i].gameObject.CompareTag("Enemy"))
            {
                // ��ų ����Ʈ
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
