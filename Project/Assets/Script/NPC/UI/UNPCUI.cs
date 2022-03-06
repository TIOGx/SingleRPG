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
    [SerializeField]
    private Image NPCImage;

    // NPC 인스턴스에 있는 Accept, reject와 바인딩 하기 위해서 public으로 선언
    public Button Button_Accept, Button_Reject;
    public Sprite[] NPCImageArr = new Sprite[5];

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
    public void EndUI() { Destroy(gameObject); NPCBase.instance.Init_QUI = false; }
    public JsonData GetNPCData() { return NPCData; }
 
    // Next 버튼 눌렀을 때
    public void OnClicked_Next()
    {
        NPC_FirstTalk.SetActive(false);
        ProccessToQuest();
    }
    // Accept 버튼 눌렀을 때
    public void OnClicked_Accept() 
    {
        QuestManager.instance.UpdateQuestUI();
    }
    // Reject 버튼 눌렀을 때
    public void OnClicked_Reject() 
    {
        EndUI();
  
        if (QuestManager.instance.getQuestQueue().Peek().questType == 2)
        {
            QuestManager.instance.getQuestQueue().Peek().nowstate1 = 0;
        }
    }
    // Exit 버튼 눌렀을 때
    public void OnClicked_Exit() 
    {
        EndUI();
    }
    // Complete 버튼 눌렀을 때
    public void OnClicked_Completed() 
    {
        SetCompensation();
        QuestManager.instance.ResetProgressUI();
        QuestManager.instance.ChangeToNextQuest(QuestManager.instance.nowQuest.toQuest);
        EndUI();

    }

    public void SetText_Title(string str) { Text_Title.GetComponent<Text>().text = str; }
    public void SetText_Desc(string str) { Text_Desc.GetComponent<Text>().text = str; }
   
    // Npc가 퀘스트를 주지 않는 기본 상태일 경우
    public void Idle() 
    {
        NPC_FirstTalk.SetActive(false);
        SetText_Title(NPCData["name"].ToString());
        SetText_Desc("아직 " + NPCData["name"].ToString() + "의 퀘스트를 받을 차례가 아니야");
        NPC_IdleTalk.SetActive(true);
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

    // UI에 들어갈 Text
    public void SetText() 
    {
        if (QuestManager.instance.getQuestQueue() == null) { return; }

        //npc 이미지 변경
        NPCImage.sprite = NPCImageArr[int.Parse(NPCData["id"].ToString()) - 1];

        // 지금 퀘스트를 진행중인 npc가 아닐 경우 idle 상태 
        if (NPCData["isProcessing"].ToString() == "False") { Idle();  return;}

        SetText_Title(NPCData["title"].ToString());
        // 완료되지 않은 퀘스트일 경우
        if (QuestManager.instance.getQuestQueue().Peek().iscompleted == false)
        {      
            SetText_Desc(NPCData["desc"][QuestManager.instance.nextIdx].ToString());
        }
        // 완료된 퀘스트일 경우
        else if (QuestManager.instance.getQuestQueue().Peek().iscompleted == true) 
        {
            SetText_Desc(QuestManager.instance.getQuestQueue().Peek().completed_text);
            // 다음 퀘스트 띄우기
            QuestManager.instance.nextIdx++;
        }
    }

    // 보상 받는 함수
    public void SetCompensation() { 
        Item getItem = QuestManager.instance.compensationItemArr[QuestManager.instance.nowQuest.compensation_ItemID].transform.GetComponent<ItemPickUp>().item;
        if (getItem.itemId == 2 ) // getItem이 돈일 때
        {
            PlayerInfo.instance.playerMoneyText.text = (int.Parse(PlayerInfo.instance.playerMoneyText.text) +int.Parse(QuestManager.instance.nowQuest.compensation_ItemNum.ToString())).ToString();
            Inventory.instance.goldText.text = (int.Parse(Inventory.instance.goldText.text) + int.Parse(QuestManager.instance.nowQuest.compensation_ItemNum.ToString())).ToString(); 
            //PlayerInfoUI.instance.playerMoney.text = QuestManager.instance.nowQuest.compensation_ItemNum.ToString();
        }
        else
        {
            Inventory.instance.AcquireItem(getItem, QuestManager.instance.nowQuest.compensation_ItemNum);
            ItemDataUI.instance.InstantiateItemDataUI(QuestManager.instance.nowQuest.compensation_ItemName.ToString(), QuestManager.instance.nowQuest.compensation_ItemNum.ToString());
        }
        
    }

    // 퀘스트 내용 띄우는 함수
    public void ProccessToQuest() 
    {
        if (QuestManager.instance.getQuestQueue() == null) { return; }

        SetText_Title(QuestManager.instance.getQuestQueue().Peek().title);
        // 완료되지 않은 퀘스트일 경우
        if (QuestManager.instance.getQuestQueue().Peek().iscompleted == false)
        {
            SetText_Desc( QuestManager.instance.getQuestQueue().Peek().desc);
            NPC_QuestTalk.SetActive(true); // 퀘스트 UI 버튼 active
        }
        // 완료된 퀘스트일 경우
        else if (QuestManager.instance.getQuestQueue().Peek().iscompleted == true) 
        {
            SetText_Desc(QuestManager.instance.getQuestQueue().Peek().completed_text);
            NPC_QuestCompleteTalk.SetActive(true);  // 퀘스트 완료 UI 버튼 active
        }
    }
}
