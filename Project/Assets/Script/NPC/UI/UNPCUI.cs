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
    private GameObject NPC_FirstTalk, NPC_QuestTalk, NPC_QuestCompleteTalk;
    [SerializeField]
    private Button Button_Next, Button_Exit, Button_Completed;
    
    // NPC 인스턴스에 있는 Accept, reject와 바인딩 하기 위해서 public으로 선언
    public Button Button_Accept, Button_Reject;

    private bool isOpening = false;

    private JsonData NPCData;
    private List<JsonData> QuestData;
    private JsonData CurrentQuest;

    void Start()
    {
        // 그냥 버튼만 끌어서 넣어주고 이벤트 리스너에 코드로 직접 함수 바인딩 해주기
        //버튼이라는 객체의 이벤트 속성으로 onClick,onHover 뭐시기 다 있을거니까
        //이벤트의 "함수이름"으로 연결해주기 : 사실상 이 과정이 유니티 툴에서 버튼으로 함수 연결하는 과정임
        Button_Next.onClick.AddListener(OnClicked_Next);  
        Button_Accept.onClick.AddListener(OnClicked_Accept);  
        Button_Reject.onClick.AddListener(OnClicked_Reject);  
        Button_Exit.onClick.AddListener(OnClicked_Exit);
        Button_Completed.onClick.AddListener(OnClicked_Completed);
    }

    // UI 생성 직후 바로 SetData 해주기
    public void SetData(JsonData InNPCData, List<JsonData> InInQuestData)
    {
        NPCData = InNPCData;
        QuestData = InInQuestData;
        CurrentQuest = QuestData[0];

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
       
        ProccessToQuest();
    }

    public void OnClicked_Accept()
    {

    }

    public void OnClicked_Reject()
    {
        EndUI();
    }

    public void OnClicked_Exit()
    {
        EndUI();
    }

    public void OnClicked_Completed()
    {
        // 보상 주는 함수 짜기
        EndUI();
    }

    public void ProccessToQuest()
    {
        // 테스트 임시로 0번재 퀘스트를 띄움
        Text_Title.GetComponent<Text>().text = QuestData[1]["message"]["title"].ToString();
        Text_Desc.GetComponent<Text>().text = QuestData[1]["message"]["desc"].ToString();

        if(QuestData[1]["type"].ToString() == "1") // 퀘스트 내용
        {
            NPC_QuestTalk.SetActive(true);
        }   
        else if (QuestData[1]["type"].ToString() == "2")// 퀘스트 완료
        {
            NPC_QuestCompleteTalk.SetActive(true);
        }

           
    }

    public JsonData GetCurrentQuestData()
    {
        return CurrentQuest;
    }

    public void EndUI()
    {
        Destroy(gameObject);
    }
}
