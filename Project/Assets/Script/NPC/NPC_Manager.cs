using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;
using UnityEngine;

namespace NPCManager{
    // NPC관련한 모든 interface와 class를 여기에 작성

    public enum ENPCType{
        // NPCData의 type에 대응하는 NPC Type 
        // 그러면 Quest_MGR 에도 QuestType이 있어야겠죠?
        Type1 = 1, // 
        Type2 = 2,
        Type3 = 3,
    }

    // for NPC class
    public interface INPC {
        List<int> QuestList {get; set;}
        GameObject NPCUI {get; set;}

        // Interface를 상속받는 객체들의 함수 구체화는 public 으로 해야함
        void Accept(); 
        void ProccessAccept(int InNPC_ID, int InQ_ID);
        // 어떤 NPC의 어떤 Quest를 수락인지 알아야함. 위에 걸로 할 수 있지않냐?
        // AddListener로 바인딩 시켜줄때 파라미터 있으면 안되어서 Accept안에다가 넣어줘야함.

        void Reject();
        void GivePlayerCompensation(); //보상주기

        // NPC UI Start를 클릭으로 할건지 Collider로 할건지 정하기 여기선 테스트로 Col 사용
        void OnCollisionEnter(Collision other);
        void InitUI(int NPC_ID);
    }

    // for NPC UI class
    public interface INPCUI {

        void SetData(JsonData InNPCData, List<JsonData> InInQuestData);
        JsonData GetCurrentQuestData();
        void SetText();
        void OnClicked_Next();
        void ProccessToQuest();
        void OnClicked_Exit();
        void OnClicked_Accept();
        void OnClicked_Reject();
        void EndUI();


    }

    public class NPC_Manager : MonoBehaviour
    {
    }

    public class NPCBase : MonoBehaviour, INPC
    {
        public static NPCBase instance;
        private GameObject QuestUI; // NPCUI Prefab을 넣어주기
        private UNPCUI UI;

        //protected int NPC_ID;
        public int NPC_ID;
        private List<int> _QuestList;        
        private GameObject _NPCUI;
        public JsonData NPCJsonData;
        private string jsonString;
        public bool Init_QUI = false;

        private void Awake()
        {
            instance = this;
        }
        public List<int> QuestList
        {
            get => _QuestList;
            set => _QuestList = value;
        }
        public GameObject NPCUI {
            get => _NPCUI;
            set => _NPCUI = value;
        }

        protected virtual void Start()
        {
            jsonString = File.ReadAllText(Application.dataPath + "/Data/NPC_Data/NPCData.json");
            if (jsonString == "") { return; }
            NPCJsonData = JsonMapper.ToObject(jsonString);

            // 이렇게 코드에서 로드하려면 꼭 "Assets/Resources" 하위에다가 프리팹을 만들어줘야함
            QuestUI = Resources.Load<GameObject>("Prefab/UI/UNPCUI");
        }

        public virtual void Accept()
        {
            ProccessAccept(NPC_ID, 1);
        }

        public virtual void ProccessAccept(int InNPC_ID, int InQ_ID)
        {
            UI.EndUI();
        }

        public void CanProgressQuest(bool tf) { }
        public void CanGetcompensationQuest(bool tf) { }
        public void Reject()
        {
            
        }

        public void GivePlayerCompensation() //보상주기
        {
            
        }

        public void OnCollisionEnter(Collision other) 
        {        
            if(UI != null) { return; } //UI 중복으로 안되게

            //InitUI(NPC_ID);
        }

        public void InitUI(int NPC_ID)
        {
            if (!Init_QUI)
            {
                Init_QUI = true;
                UI = Instantiate(QuestUI).GetComponent<UNPCUI>();
                // 여기서 UI 생성 시키고 UI라는 변수로 물고있고
                // Obj의 UNPCUI컴포에 접근해서 해당 클래스 함수들 사용하기

                // public으로 선언된 버튼 2개에 NPC오브젝트의 실제 기능들 바인딩
                UI.Button_Accept.onClick.AddListener(Accept);
                UI.Button_Reject.onClick.AddListener(Reject);

                if (UI == null) { return; }

                jsonString = File.ReadAllText(Application.dataPath + "/Data/Quest_Data/QuestData_" + NPC_ID.ToString() + ".json");
                if (jsonString == "") { return; }
                JsonData QuestJsonData = JsonMapper.ToObject(jsonString);

                List<JsonData> QuestArray = new List<JsonData>();
                QuestArray.Add(QuestJsonData);

                UI.SetData(NPCJsonData[NPC_ID.ToString()], QuestArray);
            }      
        }
    }
}

