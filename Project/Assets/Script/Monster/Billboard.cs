using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    Transform cam;
    private void Start()
    {
        cam = Camera.main.transform;
    }
    private void Update()
    {
        transform.LookAt(transform.position + cam.rotation * Vector3.forward, cam.rotation * Vector3.up); // 항상 카메라가 정면으로 바라볼 수 있도록 로테이션 값이 조정 
    }
}
