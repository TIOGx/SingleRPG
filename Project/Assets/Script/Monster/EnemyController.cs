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
    GameObject Item_0001;

    public GameObject thisObject;

    private void Awake() {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        nav = GetComponent<NavMeshAgent>();
        
    }

    void Start()
    {
        Item_0001 = Resources.Load("Prefab/Item/Item_0001") as GameObject;
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
        if(other.gameObject.tag == "Player"){
            animator.SetTrigger("Attack");
            Debug.Log("Attakc!!!");
        }   
    }

    void die(){
        animator.SetTrigger("Died");
    }

    public void ItemDrop(GameObject gameObject, Transform transform)
    {
        Instantiate(gameObject, transform.position, Quaternion.identity);
    }

    public void Died(){
        ObjectpoolManager.Instance.ReturnObject(thisObject.GetComponent<Monster>());
        Debug.Log("아이템 드롭!");
        ItemDrop(Item_0001, gameObject.transform);
    }

  
    IEnumerator WaitForIt(float delayTime)
    {
        Debug.Log("5초만 기다려");
        yield return new WaitForSeconds(delayTime);
    }
}
