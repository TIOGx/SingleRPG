using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public static WeaponController instance;
    public BoxCollider SwordArea;
    public int damage;
    public TrailRenderer TrailEffect;
    public bool attackable;
    // Start is called before the first frame update
    private void Awake()
    {
        attackable = false;
        instance = this;
    }
    // TrailEffect.Emitting 을 통해 공격시에만 이펙트 켜기
    // Update is called once per frame
    void Update()
    {
        
    }
    public void TurnOnWeaponEffect()
    {
        TrailEffect.emitting = true;
        attackable = true;
    }
    public void TurnOffWeaponEffect()
    {
        TrailEffect.emitting = false;
        attackable = false;
    }
}
