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
    private GameObject NPC_FirstTalk, NPC_QuestTalk, NPC_QuestCompleteTalk;
    [SerializeField]
    private Button Button_Next, Button_Exit, Button_Completed;


    // NPC 인스턴스에 있는 Accept, reject와 바인딩 하기 위해서 public으로 선언
    public Button Button_Accept, Button_Reject;

    private bool isOpening = false;

    private JsonData NPCData;
    private List<JsonData> QuestData;
    private JsonData CurrentQuest;

    private void Awake()
    {
        instance = this;
    }

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

    public void SetText()
    {
        if (QuestManager.instance.getQuestQueue() == null) { return; }

        Text_Title.GetComponent<Text>().text = NPCData["title"].ToString();
        if (QuestManager.instance.getQuestQueue().Peek().iscompleted == false)
        {
            Text_Desc.GetComponent<Text>().text = NPCData["desc"][QuestManager.instance.nextIdx].ToString();
        }
        else if (QuestManager.instance.getQuestQueue().Peek().iscompleted == true) // 다음 퀘스트 띄우기
        {
            Text_Desc.GetComponent<Text>().text = NPCData["completed_text"][QuestManager.instance.nextIdx].ToString();
            QuestManager.instance.nextIdx++;
        }
    }

    public void OnClicked_Next()
    {
        Debug.Log("퀘스트 내용 띄우기");
        NPC_FirstTalk.SetActive(false);
        ProccessToQuest();
    }

    public void OnClicked_Accept()
    {
        QuestManager.instance.UpdateQuestUI();
        Debug.Log("퀘스트 수락");
    }

    public void OnClicked_Reject()
    {
        EndUI();
        Debug.Log("퀘스트 거절");
    }

    public void OnClicked_Exit()
    {
        EndUI();
        Debug.Log("퀘스트 exit");
    }

    public void OnClicked_Completed()
    {
        Debug.Log("퀘스트 완료, 보상 받기");
        SetCompensation();
        QuestManager.instance.ResetProgressUI();
        QuestManager.instance.ChangeToNextQuest(QuestManager.instance.nowCompleteIdx);
        EndUI();

    }

    public void SetCompensation() { // 보상 받는 함수
        Debug.Log("보상 받는 함수 실행");
        Debug.Log(QuestManager.instance.nowQuest.questIdx);
        
        Item getItem = QuestManager.instance.compensationItemArr[QuestManager.instance.nowQuest.compensation_ItemID].transform.GetComponent<ItemPickUp>().item;
        Inventory.instance.AcquireItem(getItem, QuestManager.instance.nowQuest.compensation_ItemNum);
        ItemDataUI.instance.InstantiateItemDataUI(QuestManager.instance.nowQuest.compensation_ItemName.ToString(), QuestManager.instance.nowQuest.compensation_ItemNum.ToString());
    }

    public void ProccessToQuest() // 퀘스트 내용 띄우기
    {
        if (QuestManager.instance.getQuestQueue() == null) { return; }

        Text_Title.GetComponent<Text>().text = QuestManager.instance.getQuestQueue().Peek().title;
        if (QuestManager.instance.getQuestQueue().Peek().iscompleted == false)
        {
            Text_Desc.GetComponent<Text>().text = QuestManager.instance.getQuestQueue().Peek().desc;
            NPC_QuestTalk.SetActive(true);
        }
        else if (QuestManager.instance.getQuestQueue().Peek().iscompleted == true) 
        {
            Text_Desc.GetComponent<Text>().text = QuestManager.instance.getQuestQueue().Peek().completed_text;
            NPC_QuestCompleteTalk.SetActive(true);
        }
    }

}
