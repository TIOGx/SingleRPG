using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NPCManager; // NPC_Manager.cs 의 NPCManager namespace를 using 해서(import 느낌) 밑에서 INPC_UI를 상속받아서 사용

public class UNPCUI_Manager : MonoBehaviour, INPCUI
{
    [SerializeField]
    private Text Text_Title, Text_Desc;
    
    void Start()
    {
        
    }

    // UI 생성 직후 바로 SetText 해주기
    public void SetText(UNPCUIText InData)
    {
        Text_Title.GetComponent<Text>().text = InData.Title;
        Text_Desc.GetComponent<Text>().text = InData.Desc;
    }

    public void OnClicked_Exit()
    {
        Destroy(this);
    }

    public void OnClicked_Accept()
    {
        Destroy(this);
    }

    public void OnClicked_Reject()
    {
        Destroy(this);
    }
}
