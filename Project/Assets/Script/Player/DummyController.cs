using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyController : MonoBehaviour
{
    public static DummyController instance;

    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Transform characterBody;
    [SerializeField]
    private Transform cameraArm;
    [SerializeField]
    private bool isDiveDelay;
    [SerializeField]
    private bool isAttackDelay;
    [SerializeField]
    private bool isJumpDelay;

    public GameObject hudDamageText;
    public Transform hudPos;
    private int attackNum;
    // 잠시 가라 포션
    public GameObject Hp_Potion_Prefab;
    

    public float moveSpeed { set; get; }
    public float rotationSpeed { set; get; }
    public float normalSpeed { set; get; }
    public float runSpeed { set; get; }

    Rigidbody m_Rigidbody;

    //Coroutine 함수
    IEnumerator setDiveDelay(float delayTime)
    {
        Debug.Log("delay " + delayTime + " time");
        yield return new WaitForSeconds(delayTime);
        isDiveDelay = false;
    }

    IEnumerator setAttackDelay(float delayTime)
    {
        Debug.Log("delay " + delayTime + " time");
        yield return new WaitForSeconds(delayTime);
        isAttackDelay = false;
    }

    IEnumerator setJumpDelay(float delayTime)
    {
        Debug.Log("delay " + delayTime + " time");
        yield return new WaitForSeconds(delayTime);
        isJumpDelay = false;
    }

    // Life Cycle 함수

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        animator = characterBody.GetComponent<Animator>();
        normalSpeed = 7.0f;
        runSpeed = 12.0f;
        attackNum = 0;
        isDiveDelay = false;
        isAttackDelay = false;
        isJumpDelay = false;
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        DiveRoll();
        Attack();
        Jump();
        UsePotion();
    }

    void FixedUpdate()
    {
        Move();
    }


    // move & run 함수
    private void Move()
    {
        Debug.DrawRay(cameraArm.position, new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized, Color.red);
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        bool isMove = moveInput.magnitude > 0;
        animator.SetBool("isMove", isMove);

        if (isMove)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                animator.SetBool("isRun", true);
                moveSpeed = runSpeed;
            }
            else
            {
                animator.SetBool("isRun", false);
                moveSpeed = normalSpeed;
            }
            Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
            Vector3 lookright = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
            Vector3 moveDir = lookForward * moveInput.y + lookright * moveInput.x;

            characterBody.forward = moveDir;
            transform.position += moveDir * Time.deltaTime * moveSpeed;
        }

    }

    // dive 함수
    private void DiveRoll()
    {
        if (!isDiveDelay)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isDiveDelay = true;
                animator.SetTrigger("DiveRoll");
                StartCoroutine(setDiveDelay(3.0f));
            }
        }
    }

    private void Attack()
    {
        if (!isAttackDelay)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                isAttackDelay = true;
                if (attackNum % 3 == 0)
                {
                    animator.SetTrigger("Attack");
                    attackNum = attackNum / 3 + 1;

                }
                else if (attackNum % 3 == 1)
                {
                    animator.SetTrigger("Attack2");
                    attackNum += 1;
                }
                else
                {
                    animator.SetTrigger("Attack3");
                    attackNum += 1;
                }
                StartCoroutine(setAttackDelay(1.0f));
               
            }

        }
    }

    // jump 함수
    private void Jump()
    {
        if (!isJumpDelay)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                isJumpDelay = true;
                animator.SetTrigger("isJump");
                m_Rigidbody.AddForce(transform.up * 80f);
                StartCoroutine(setJumpDelay(3.0f));
                
            }

        }
    }
    // 포션 먹기 함수 일단 인벤토리에 있을 때 먹는 함수 작성해볼 예정
    private void UsePotion()
    {
        // 키 입력을 받음
        if (Input.GetKeyDown(KeyCode.Y))
        {
            Debug.Log("키 눌렸어요 포션먹는 키");
            if (Inventory.instance.CheckItem(Hp_Potion_Prefab.GetComponent<ItemPickUp>().item)){ // 포션이 존재하면서 피가 달았으면
                if(PlayerInfo.instance.CurrentHealth < PlayerInfo.instance.MaxHealth)
                {
                    // 체력을 회복하고
                    PlayerInfo.instance.HealPlayer(50);
                    UserInterface.instance.UpdateHpBarUI(PlayerInfo.instance.CurrentHealth / PlayerInfo.instance.MaxHealth);
                    UserInterface.instance.NowHp.text = PlayerInfo.instance.CurrentHealth.ToString();

                    // 인벤토리에서 갯수를 줄인다.
                    Inventory.instance.UseItem(Hp_Potion_Prefab.GetComponent<ItemPickUp>().item);
                    PlayerInfo.instance.playerPotionText.text = (int.Parse(PlayerInfo.instance.playerPotionText.text) - int.Parse(1.ToString())).ToString();
                }
            }
        }
    }
    

    public Transform GetPlayerTransform()
    {
        return this.transform;
    }

    public void killMonster(int Monterid)
    {
        Debug.Log("이거 죽였엄");
        Debug.Log(Monterid);
        PlayerInfo.instance.GetExp(10); // 임시로 박아 놓은 임의 경험치 값
        QuestManager.instance.checkQuest(1, Monterid); // 퀘스트 타입: 사냥, 몬스터 id
    }

    public void TakeDamage(float value)
    {
        PlayerInfo.instance.CurrentHealth -= value;
        UserInterface.instance.NowHp.text = PlayerInfo.instance.CurrentHealth.ToString();
        UserInterface.instance.UpdateHpBarUI(PlayerInfo.instance.CurrentHealth / PlayerInfo.instance.MaxHealth);

        GameObject hudText = Instantiate(hudDamageText); // 생성할 텍스트 오브젝트
        hudText.transform.position = hudPos.position; // 표시될 위치
        hudText.GetComponent<FloatingText>().damage = value; // 데미지 전달
        if (PlayerInfo.instance.CurrentHealth <= 0)
        {
            PlayerInfo.instance.CurrentHealth = 0;
            UserInterface.instance.NowHp.text = PlayerInfo.instance.CurrentHealth.ToString();
        }
        // HpBar.rectTransform.localScale = new Vector3(CurHealth / MaxHealth, 1f, 1f); // 우리 체력바랑 연동
        if (PlayerInfo.instance.CurrentHealth == 0)
        {
            Playerdie();
        }
    }

    void Playerdie()
    {
        animator.SetTrigger("isDead");
        PlayerInfo.instance.playerDieCanvas.SetActive(true);
        Pause.Instance.TimePause();
    }
}