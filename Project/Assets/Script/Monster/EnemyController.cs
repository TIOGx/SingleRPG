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
    public GameObject hudDamageText;
    public Transform hudPos;
    public float tracingRange;
    public MonsterState NowState;
    public float AttackRange;
    public bool IsAttackDelay;
    public float AttackDelay;
    public float power;
    public enum MonsterState
    {
        idle,
        tracing,
        die
    }
    private void Awake() {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        nav = GetComponent<NavMeshAgent>();
        IsAlive = true;
        IsAttackDelay = false;


    }

    void Start()
    {
        Target = DummyController.instance.GetPlayerTransform();
        animator = monsterBody.GetComponent<Animator>();
        NowState = MonsterState.idle;
    }

    // Update is called once per frame
    void Update()
    {
        if(NowState != MonsterState.die)
        {
            Tracing();
            StartCoroutine("AttackPlayer");
        }
           
    }
    void Tracing()
    {
        if (IsTracing()) // 거리가 되느냐
        {
            NowState = MonsterState.tracing;
            nav.SetDestination(Target.position);
        }
        else
        {
            NowState = MonsterState.idle;
        }
    }
    void OnTriggerEnter(Collider other) {
        Debug.Log("Who");
        if (!IsAlive){ return; }
        if(other.tag == "Weapon"){
            WeaponController Weapon = other.GetComponent<WeaponController>();
            if (Weapon.attackable)
            {
                animator.SetTrigger("GetHit");
                TakeDamage(PlayerInfo.instance.AttackDamage);
                Weapon.attackable = false;
            }
        }
    }
    public void TakeDamage(float value)
    {
        CurHealth -= value;
        GameObject hudText = Instantiate(hudDamageText); // 생성할 텍스트 오브젝트
        hudText.transform.position = hudPos.position; // 표시될 위치
        hudText.GetComponent<FloatingText>().damage = value; // 데미지 전달
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

    void knockback()
    {
        monsterBody.position = Vector3.Slerp(monsterBody.position, monsterBody.position - transform.forward * 2, 0.5f);
            
    }
    public bool IsTracing()
    {
        if(Vector3.Distance(Target.position, transform.position) < tracingRange)
        {
            return true;
        }
        return false;
    }
    IEnumerator AttackPlayer()
    {
        if (Vector3.Distance(Target.position, transform.position) < AttackRange && !IsAttackDelay){
            transform.LookAt(Target);
            animator.SetTrigger("Attack");
            Debug.Log("Attakc!!!");
            IsAttackDelay = true;
            DummyController.instance.TakeDamage(power);
            yield return new WaitForSeconds(AttackDelay);
            IsAttackDelay = false;
        }
    }


    void die(){
        IsAlive = false;
        animator.SetTrigger("Died");
        SignalToPlayer(MonsterId);
        NowState = MonsterState.die;
        nav.SetDestination(transform.position);

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
        NowState = MonsterState.idle;
    }

    public void Died(){
        Debug.Log("몬스터 죽음");
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
