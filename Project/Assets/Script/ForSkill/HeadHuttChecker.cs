using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadHuttChecker : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (BossController.instance.IsHeadHutt == false)
        {
            return;
        }
        if (collision.gameObject.tag == "Player")
        {
            DummyController.instance.TakeDamage(10f);
            BossController.instance.IsHeadHutt = true;
        }
    }
}
