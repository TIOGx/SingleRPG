using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using NPCManager; // NPC_Manager.cs 의 NPCManager namespace를 using 해서(import 느낌) 밑에서 INPC_UI를 상속받아서 사용

public class UNPCUI : MonoBehaviour, INPCUI
{
    public static UNPCUI instance;
    [SerializeField]
    private Text Text_Title, Text_Desc;
    [SerializeField]
    private GameObject NPC_FirstTalk, NPC_QuestTalk, NPC_QuestCompleteTalk, NPC_IdleTalk;
    [SerializeField]
    private Button Button_Next, Button_Exit, Button_Completed;


    // NPC 인스턴스에 있는 Accept, reject와 바인딩 하기 위해서 public으로 선언
    public Button Button_Accept, Button_Reject;

    private JsonData NPCData;
    private List<JsonData> QuestData;
    private JsonData CurrentQuest;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Button_Next.onClick.AddListener(OnClicked_Next);
        Button_Accept.onClick.AddListener(OnClicked_Accept);
        Button_Reject.onClick.AddListener(OnClicked_Reject);
        Button_Exit.onClick.AddListener(OnClicked_Exit);
        Button_Completed.onClick.AddListener(OnClicked_Completed);
    }

    public JsonData GetCurrentQuestData() { return CurrentQuest; }
    public void EndUI() { Destroy(gameObject); }
    public JsonData GetNPCData() { return NPCData; }


    // UI 생성 직후 바로 SetData 해주기
    public void SetData(JsonData InNPCData, List<JsonData> InInQuestData)
    {
        NPCData = InNPCData;
        QuestData = InInQuestData;
        CurrentQuest = QuestData[0];

        //데이터 받아와서 파싱후 알맞게 넣어주기
        SetText();
    }
    public void OnClicked_Next() // next 버튼 눌렀을 때
    {
        NPC_FirstTalk.SetActive(false);
        ProccessToQuest();
    }

    public void OnClicked_Accept() // accept 버튼 눌렀을 때
    {
      
        QuestManager.instance.UpdateQuestUI();
    }

    public void OnClicked_Reject() // reject 버튼 눌렀을 때
    {
        EndUI();
    }

    public void OnClicked_Exit() // exit 버튼 눌렀을 때
    {
        EndUI();
    }

    public void OnClicked_Completed() //complete 버튼 눌렀을 때
    {
        SetCompensation();
        QuestManager.instance.ResetProgressUI();
        QuestManager.instance.ChangeToNextQuest(QuestManager.instance.nowQuest.toQuest);
        EndUI();

    }

    public void SetText_Title(string str) { Text_Title.GetComponent<Text>().text = str; }
    public void SetText_Desc(string str) { Text_Desc.GetComponent<Text>().text = str; }

    public void Idle() // Npc가 퀘스트를 주지 않는 기본 상태일 경우
    {
        SetText_Title(NPCData["name"].ToString());
        SetText_Desc("안녕! 나는 " + NPCData["name"].ToString() + "야!");
        NPC_IdleTalk.SetActive(true);
    }

    public void SetText()
    {
        if (NPCData["isProcessing"].ToString() == "False") {  Idle();  return;}
        if (QuestManager.instance.getQuestQueue() == null) { return; }

        SetText_Title(NPCData["title"].ToString());
        if (QuestManager.instance.getQuestQueue().Peek().iscompleted == false)
        {
            SetText_Desc(NPCData["desc"][QuestManager.instance.nextIdx].ToString());
        }
        else if (QuestManager.instance.getQuestQueue().Peek().iscompleted == true) // 다음 퀘스트 띄우기
        {
            SetText_Desc(NPCData["completed_text"][QuestManager.instance.nextIdx].ToString());
            QuestManager.instance.nextIdx++;
        }
    }

   

    public void SetCompensation() { // 보상 받는 함수
        Debug.Log("보상 받는 함수 실행");
        Debug.Log(QuestManager.instance.nowQuest.questIdx);
        
        Item getItem = QuestManager.instance.compensationItemArr[QuestManager.instance.nowQuest.compensation_ItemID].transform.GetComponent<ItemPickUp>().item;
        Inventory.instance.AcquireItem(getItem, QuestManager.instance.nowQuest.compensation_ItemNum);
        ItemDataUI.instance.InstantiateItemDataUI(QuestManager.instance.nowQuest.compensation_ItemName.ToString(), QuestManager.instance.nowQuest.compensation_ItemNum.ToString());
    }
   
    public void ProccessToQuest() // 퀘스트 내용 띄우는 함수
    {
        if (QuestManager.instance.getQuestQueue() == null) { return; }

        SetText_Title(QuestManager.instance.getQuestQueue().Peek().title);
        if (QuestManager.instance.getQuestQueue().Peek().iscompleted == false)
        {
            SetText_Desc( QuestManager.instance.getQuestQueue().Peek().desc);
            NPC_QuestTalk.SetActive(true);
        }
        else if (QuestManager.instance.getQuestQueue().Peek().iscompleted == true) 
        {
            SetText_Desc(QuestManager.instance.getQuestQueue().Peek().completed_text);
            NPC_QuestCompleteTalk.SetActive(true);
        }
    }


}
