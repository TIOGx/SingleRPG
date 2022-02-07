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

    private int attackNum;

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

    // 충돌 함수
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Npc"))
        {
            //animator.SetTrigger("Talk");
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
        PlayerInfo.instance.GetExp(3); // 임시로 박아 놓은 임의 경험치 값
        QuestManager.instance.checkQuest(1, Monterid); // 퀘스트 타입: 사냥, 몬스터 id
    }
}