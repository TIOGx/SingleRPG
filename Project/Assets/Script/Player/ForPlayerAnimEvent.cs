using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForPlayerAnimEvent : MonoBehaviour
{
    public void TurnOnWeaponEffect()
    {
        Debug.Log("���� ����Ʈ �ѱ�");
        WeaponController.instance.TurnOnWeaponEffect();
    }
    public void TurnOffWeaponEffect()
    {
        WeaponController.instance.TurnOffWeaponEffect();
    }
}
