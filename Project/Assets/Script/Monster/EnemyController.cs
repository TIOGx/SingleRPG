using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

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
    public GameObject [] DropItem;
    public float[] DropItemPercentage;
    private bool IsAlive;
    public GameObject thisObject;
    public int MonsterId = 0;
    public Image HpBar;
    public GameObject RandomDropItem;
    

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
            TakeDamage(Weapon.damage);
        }
    }
    void TakeDamage(int value)
    {
        CurHealth -= value;
        // Debug.Log(CurHealth);
        if (CurHealth < 0)
        {
            CurHealth = 0;
        }
        HpBar.rectTransform.localScale = new Vector3(CurHealth / MaxHealth, 1f, 1f);
        if (CurHealth == 0)
        {
            die();
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
    void OnEnable()
    {
        initialize();
    }
    public void initialize()
    {
        CurHealth = MaxHealth;
        HpBar.rectTransform.localScale = new Vector3(CurHealth / MaxHealth, 1f, 1f);
        IsAlive = true;
    }
    public void Died(){
        ObjectpoolManager.Instance.ReturnObject(thisObject.GetComponent<Monster>());
        Debug.Log("랜덤 아이템 드롭!");
        float random_float = Random.Range(0, 100);
        Debug.Log(random_float);
        for (int i = 0; i<DropItemPercentage.Length; i++)
        {
            if(random_float <= DropItemPercentage[i])
            {
                //i번째 아이템 당첨
                Debug.Log(i);
                RandomDropItem = DropItem[i];
                break;
            }
            else
            {
                random_float -= DropItemPercentage[i];
            }
        }
        ItemDrop(RandomDropItem, gameObject.transform);
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
