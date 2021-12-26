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
    private bool isDelay;

    private int attackNum;

    public float moveSpeed {set; get;}
    public float rotationSpeed {set; get;}
    public float normalSpeed {set; get;}
    public float runSpeed {set; get;}

    Rigidbody m_Rigidbody;


    IEnumerator getDelayTime(float delayTime)
    {
        Debug.Log("delay " + delayTime + " time");
        yield return new WaitForSeconds(delayTime);
        isDelay = false;
        
    }
    private void Awake() {
        instance = this;
    }
    void Start() {
        animator = characterBody.GetComponent<Animator>();
        normalSpeed = 2.0f;
        runSpeed = 5.0f;
        attackNum = 0;
        isDelay = false; 
        m_Rigidbody = GetComponent<Rigidbody>();
    }
    void Update(){
        DiveRoll();
        Attack();
        Jump();
    }
    
    void FixedUpdate() {
        Move();
    }


    private void Move(){
        Debug.DrawRay(cameraArm.position, new Vector3(cameraArm.forward.x , 0f, cameraArm.forward.z).normalized, Color.red);
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        bool isMove = moveInput.magnitude > 0;
        animator.SetBool("isMove", isMove);
        
        if(isMove){
            if(Input.GetKey(KeyCode.LeftShift))
            {
                animator.SetBool("isRun", true);
                moveSpeed = runSpeed;
            }
            else{
                animator.SetBool("isRun", false);
                moveSpeed = normalSpeed;
            }
            Vector3 lookForward = new Vector3(cameraArm.forward.x , 0f, cameraArm.forward.z).normalized;
            Vector3 lookright = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
            Vector3 moveDir = lookForward * moveInput.y + lookright * moveInput.x;
            
            characterBody.forward = moveDir;
            transform.position += moveDir * Time.deltaTime * moveSpeed;
        }
        
    }

    private void DiveRoll(){
        if (!isDelay)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isDelay = true;
                animator.SetTrigger("DiveRoll");
                StartCoroutine(getDelayTime(3.0f));
            }
        }
    }
    private void Attack(){
        if (!isDelay){
            if (Input.GetKeyDown(KeyCode.Q)){
                isDelay = true;
                if (attackNum % 3 == 0){
                    animator.SetTrigger("Attack");
                    attackNum = attackNum / 3 + 1;

                }
                else if (attackNum % 3 == 1){
                    animator.SetTrigger("Attack2");
                    attackNum += 1;
                }
                else{
                    animator.SetTrigger("Attack3");
                    attackNum += 1;
                }
                StartCoroutine(getDelayTime(1.0f));
            }
        }
    }

    private void Jump(){
        if (!isDelay)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                isDelay = true;
                animator.SetTrigger("isJump");
                m_Rigidbody.AddForce(transform.up * 80f);
                StartCoroutine(getDelayTime(3.0f));
            }
        }
    }

    void OnCollisionEnter(Collision col)
    {
        // if (col.gameObject.CompareTag("Enemy"))
        // {
        //     animator.SetTrigger("isDead");
        // }
        if (col.gameObject.CompareTag("Npc"))
        {
            animator.SetTrigger("Talk");
        }
    }
    public Transform GetPlayerTransform(){
        return this.transform;
    }
}
