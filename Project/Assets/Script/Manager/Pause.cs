using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    bool IsPause;
    // Start is called before the first frame update
    void Start()
    {
        IsPause = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            // 일시정지 활성화
            if(IsPause == false)
            {
                Time.timeScale = 0;
                IsPause = true;
                return;
            }
            // 일시정지 비활성화
            else
            {
                Time.timeScale = 1;
                IsPause = false;
                return;
            }
        }
    }
}
