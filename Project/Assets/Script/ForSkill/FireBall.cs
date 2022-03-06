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
        Debug.Log("???? ?????????? UseSkill");
        Vector3 PlayerPos = DummyController.instance.transform.position; // ???????? ????
        Collider[] hitCol = Physics.OverlapSphere(PlayerPos, SkillRange); // ???? ???? ?? ???????? ????
        for (int i = 0; i < hitCol.Length; i++)
        {
            Debug.Log(hitCol[i].gameObject.name);
            if (hitCol[i].gameObject.CompareTag("Enemy"))
            {
                Instantiate(SkillEffectPrefab, new Vector3(hitCol[i].gameObject.transform.position.x, hitCol[i].gameObject.transform.position.y + 2.0f, hitCol[i].gameObject.transform.position.z), Quaternion.identity);
                
                hitCol[i].gameObject.GetComponent<EnemyController>().TakeDamage(SkillDamage);
            }
            else{
                continue;
            }
        }
        return;
    }
}
