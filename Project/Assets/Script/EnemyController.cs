using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float MaxHealth;
    public float CurHealth;
    Rigidbody rigid;
    BoxCollider boxCollider;
    public Transform Target;
    NavMeshAgent nav;

    private void Awake() {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        nav = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        nav.SetDestination(Target.position);
    }
    void OnTriggerEnter(Collider other) {
        if(other.tag == "Weapon"){
            WeaponController Weapon = other.GetComponent<WeaponController>();
            CurHealth -= Weapon.damage;
            Debug.Log(CurHealth);
        }
    }
}
