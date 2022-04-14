using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBallHitChecker : MonoBehaviour
{
    public float SkillDamage;
    private void Update()
    {
        SkillDamage = 2 * PlayerInfo.instance.MagicDamage;
    }
    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Enemy")
        {
            if (other.name == "Boss")
            {
                other.GetComponent<BossController>().BossTakeDamage(SkillDamage);
            }
            else
            {
                other.GetComponent<EnemyController>().TakeDamage(SkillDamage);
            }
        }

    }
}
