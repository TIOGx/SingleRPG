using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyController : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Transform characterBody;

    [SerializeField]
    private Transform cameraArm;
    [SerializeField]
    private float CamSensitive;


    public float moveSpeed {set; get;}
    public float rotationSpeed {set; get;}


    void Start() {
        animator = characterBody.GetComponent<Animator>();
        moveSpeed = 2.0f;
    }
    void Update(){
        LookAround();
        HandsUp();
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
            Vector3 lookForward = new Vector3(cameraArm.forward.x , 0f, cameraArm.forward.z).normalized;
            Vector3 lookright = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
            Vector3 moveDir = lookForward * moveInput.y + lookright * moveInput.x;
            
            characterBody.forward = moveDir;
            transform.position += moveDir * Time.deltaTime * moveSpeed;
        }
    }
    
    private void LookAround(){
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * CamSensitive;
        Vector3 camAngle = cameraArm.rotation.eulerAngles;
        float x = (camAngle.x + mouseDelta.y);
        float y = (camAngle.y + mouseDelta.x);
        if( x < 180f){
            x = Mathf.Clamp(x, -1f, 70f);
        }
        else{
            x = Mathf.Clamp(x, 355f, 361);
        }

        cameraArm.rotation = Quaternion.Euler(x, y, camAngle.z);   
    }
    private void HandsUp(){
        if(Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("HandsUp");
        }
    }
}
