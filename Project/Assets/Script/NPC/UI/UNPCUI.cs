using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;
using UnityEngine.UI;
using NPCManager; // NPC_Manager.cs 의 NPCManager namespace를 using 해서(import 느낌) 밑에서 INPC_UI를 상속받아서 사용

public class UNPCUI : MonoBehaviour, INPCUI
{
    [SerializeField]
    private Text Text_Title, Text_Desc;
    [SerializeField]
    private GameObject NPC_FirstTalk, NPC_QuestTalk;
    [SerializeField]
    private Button Button_Next,Button_Accept, Button_Reject, Button_Exit;
    private bool isOpening = false;

    private JsonData NPCData;
    private List<JsonData> QuestData;

    void Start()
    {
        // 그냥 버튼만 끌어서 넣어주고 이벤트 리스너에 코드로 직접 함수 바인딩 해주기
        //버튼이라는 객체의 이벤트 속성으로 onClick,onHover 뭐시기 다 있을거니까
        //이벤트의 "함수이름"으로 연결해주기 : 사실상 이 과정이 유니티 툴에서 버튼으로 함수 연결하는 과정임
        Button_Next.onClick.AddListener(OnClicked_Next);  
        Button_Accept.onClick.AddListener(OnClicked_Accept);  
        Button_Reject.onClick.AddListener(OnClicked_Reject);  
        Button_Exit.onClick.AddListener(OnClicked_Exit);  
    }

    // UI 생성 직후 바로 SetData 해주기
    public void SetData(JsonData InNPCData, List<JsonData> InInQuestData)
    {
        NPCData = InNPCData;
        QuestData = InInQuestData;
        //데이터 받아와서 파싱후 알맞게 넣어주기
        SetText();
    }

    public void SetText()
    {
        Text_Title.GetComponent<Text>().text = NPCData["message"]["title"].ToString();
        Text_Desc.GetComponent<Text>().text = NPCData["message"]["desc"].ToString();
    }

    public void OnClicked_Next()
    {
        NPC_FirstTalk.SetActive(false);
        NPC_QuestTalk.SetActive(true);
        ProccessToQuest();
    }

    public void OnClicked_Accept()
    {
    }

    public void OnClicked_Reject()
    {
        Destroy(gameObject);
    }

    public void OnClicked_Exit()
    {
        Destroy(gameObject);
    }

    public void ProccessToQuest()
    {
        // 테스트 임시로 0번재 퀘스트를 띄움
        Text_Title.GetComponent<Text>().text = QuestData[0]["message"]["title"].ToString();
        Text_Desc.GetComponent<Text>().text = QuestData[0]["message"]["desc"].ToString();
    }
}
