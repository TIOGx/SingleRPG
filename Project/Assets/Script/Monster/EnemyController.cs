using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Transform monsterBody;
    public float MaxHealth;
    public float CurHealth;
    Rigidbody rigid;
    BoxCollider boxCollider;
    public Transform Target;
    NavMeshAgent nav;
    public GameObject DropItem;
    private bool IsAlive;
    public GameObject thisObject;
    public int MonsterId = 0;

    private void Awake() {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        nav = GetComponent<NavMeshAgent>();
        IsAlive = true;


    }

    void Start()
    {
        Target = DummyController.instance.GetPlayerTransform();
        animator = monsterBody.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        nav.SetDestination(Target.position);
    }

    void OnTriggerEnter(Collider other) {
        Debug.Log("Who");
        if (!IsAlive){ return; }
        if(other.tag == "Weapon"){
            WeaponController Weapon = other.GetComponent<WeaponController>();
            CurHealth -= Weapon.damage;
            Debug.Log(CurHealth);
            if(CurHealth <= 0){
                die();
            }
        }
    }

    void OnCollisionEnter(Collision other) {
        if (!IsAlive) { return; }
        if (other.gameObject.tag == "Player"){
            animator.SetTrigger("Attack");
            Debug.Log("Attakc!!!");
        }   
    }

    void die(){
        IsAlive = false;
        animator.SetTrigger("Died");
        SignalToPlayer(MonsterId);
    }

    public void ItemDrop(GameObject gameObject, Transform transform)
    {
        Instantiate(gameObject, transform.position, Quaternion.identity);
    }

    public void Died(){
        ObjectpoolManager.Instance.ReturnObject(thisObject.GetComponent<Monster>());
        Debug.Log("아이템 드롭!");
        ItemDrop(DropItem, gameObject.transform);
    }

  
    IEnumerator WaitForIt(float delayTime)
    {
        Debug.Log("5초만 기다려");
        yield return new WaitForSeconds(delayTime);
    }
    void SignalToPlayer(int id)
    {
        DummyController.instance.killMonster(id);
    }
}
