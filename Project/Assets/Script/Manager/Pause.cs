using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public static Pause Instance;
    bool IsPause;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        IsPause = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TimePause()
    {

        // �Ͻ����� Ȱ��ȭ
        if (IsPause == false)
        {
            Time.timeScale = 0;
            IsPause = true;
            return;
        }
            // �Ͻ����� ��Ȱ��ȭ
        else
        {
            Time.timeScale = 1;
            IsPause = false;
            return;
        }

    }
}
