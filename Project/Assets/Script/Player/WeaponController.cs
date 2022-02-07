using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public static WeaponController instance;
    public BoxCollider SwordArea;
    public int damage;
    public TrailRenderer TrailEffect;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    // TrailEffect.Emitting �� ���� ���ݽÿ��� ����Ʈ �ѱ�
    // Update is called once per frame
    void Update()
    {
        
    }
    public void TurnOnWeaponEffect()
    {
        TrailEffect.emitting = true;
    }
    public void TurnOffWeaponEffect()
    {
        TrailEffect.emitting = false;
    }
}
