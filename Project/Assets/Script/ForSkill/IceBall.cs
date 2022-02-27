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
        Debug.Log("�ڽ� ��ũ��Ʈ�� UseSkill");
        Vector3 PlayerPos = DummyController.instance.transform.position; // �÷��̾� ��ǥ
        Collider[] hitCol = Physics.OverlapSphere(PlayerPos, SkillRange); // ���� ���� �� �ݶ��̴� �ĺ�
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
