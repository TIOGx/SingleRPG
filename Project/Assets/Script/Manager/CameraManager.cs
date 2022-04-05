using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    private Transform centralAxis; // 중심축
    [SerializeField]
    private Transform cam;
    [SerializeField]
    private float camSpeed; // 카메라 회전 속도
    [SerializeField]
    private GameObject MainCamera;

    private float mouseX;
    private float mouseY;
    private float wheel;

    private void CameraMove()
    {
        //Debug.Log(MainCamera.transform.position.y);
         if (Input.GetMouseButton(1)) // 마우스 오른쪽 버튼 클릭
         {
            mouseX = Input.GetAxis("Mouse X") * camSpeed; // 프로젝트 세팅에 입력 값 매니저를 보면 이름이 Mouse X로 설정되어있음
            mouseY = Input.GetAxis("Mouse Y") * camSpeed;
            Vector3 camAngle = centralAxis.rotation.eulerAngles;
            float x = camAngle.x - mouseY;
            if(x < 180f)
            {
                x = Mathf.Clamp(x, 0f, 70f);
            }
            else
            {
                x = Mathf.Clamp(x, 335f, 361f);
            }

            centralAxis.rotation = Quaternion.Euler(x, camAngle.y + mouseX, camAngle.z);
         }
    }


    private void Zoom()
    {
        wheel += Input.GetAxis("Mouse ScrollWheel");
        if (wheel >= -3){wheel = -3;}
        else if(wheel <= -7){wheel = -7;}

        cam.localPosition = new Vector3(0, 0, wheel);
    }
    void Awake()
    {
        wheel =  -5;
    }
    void Update()
    {
        CameraMove();
        Zoom();
    }
}
