using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForPlayerAnimEvent : MonoBehaviour
{
    public void TurnOnWeaponEffect()
    {
        Debug.Log("공격 이펙트 켜기");
        WeaponController.instance.TurnOnWeaponEffect();
    }
    public void TurnOffWeaponEffect()
    {
        WeaponController.instance.TurnOffWeaponEffect();
    }
}
