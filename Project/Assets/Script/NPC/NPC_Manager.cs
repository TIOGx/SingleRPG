using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;
using UnityEngine;

namespace NPCManager{
    // NPC관련한 모든 interface와 class를 여기에 작성

    public enum NPCType{
        // NPCData의 type에 대응하는 NPC Type 
        // 그러면 Quest_MGR 에도 QuestType이 있어야겠죠?
        Type1 = 1,
        Type2 = 2,
        Type3 = 3,
    }

    // for NPC class
    public interface INPC {
        List<int> QuestList {get; set;}
        GameObject NPCUI {get; set;}

        // Interface를 상속받는 객체들의 함수 구체화는 public 으로 해야함
        void Accept(int NPC_ID, int Q_ID); // 어떤 NPC의 어떤 Quest를 수락인지
        void Reject(int NPC_ID, int Q_ID);
        void GivePlayerCompensation(); //보상주기

        // NPC UI Start를 클릭으로 할건지 Collider로 할건지 정하기 여기선 테스트로 Col 사용
        void OnCollisionEnter(Collision other);
        void InitUI(int NPC_ID);
    }

    // for NPC UI class
    public interface INPCUI {
        void SetData(JsonData InNPCData, List<JsonData> InInQuestData);
        void SetText();
        void OnClicked_Next();
        void ProccessToQuest();
        void OnClicked_Exit();
        void OnClicked_Accept();
        void OnClicked_Reject();
    }

    public class NPC_Manager : MonoBehaviour
    {
        // 이거 싱글톤으로 해서 어느 객체든 참조하기 쉽게 변경해용!
        
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }

    public class NPCBase : MonoBehaviour, INPC
    {
        private GameObject QuestUI; // NPCUI Prefab을 넣어주기
        private UNPCUI UI;

        protected int NPC_ID;
        private List<int> _QuestList;        
        private GameObject _NPCUI;
        private bool bIsTalking = false;
        private string jsonString;

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
            // 이렇게 코드에서 로드하려면 꼭 "Assets/Resources" 하위에다가 프리팹을 만들어줘야함
            QuestUI = Resources.Load<GameObject>("Prefab/UI/UNPCUI");
        }

        public void Accept(int NPC_ID , int Q_ID)
        {
            
        }

        public void Reject(int NPC_ID, int Q_ID)
        {
            
        }

        public void GivePlayerCompensation() //보상주기
        {
            
        }

        public void OnCollisionEnter(Collision other) 
        {        
            if(UI != null) { return; } //UI 중복으로 안되게
            InitUI(NPC_ID);
        }

        public void InitUI(int NPC_ID)
        {
            UI = Instantiate(QuestUI).GetComponent<UNPCUI>(); 
            // 여기서 UI 생성 시키고 UI라는 변수로 물고있고
            // Obj의 UNPCUI컴포에 접근해서 해당 클래스 함수들 사용하기

            if(UI == null) { return; }

            jsonString = File.ReadAllText(Application.dataPath + "/Data/NPC_Data/NPCData.json");
            if(jsonString == "") { return; }
            JsonData NPCJsonData = JsonMapper.ToObject(jsonString); 
            // NPC_ID 에 해당하는 NPC 데이터

            jsonString = File.ReadAllText(Application.dataPath + "/Data/Quest_Data/QuestData.json");
            if(jsonString == "") { return; }
            JsonData QuestJsonData = JsonMapper.ToObject(jsonString);
            // Quest 데이터는 다 불러오고 

            List<JsonData> QuestArray = new List<JsonData>();
        
            // int인걸 우리는 알지만 컴퓨터는 모름. 왜? 여전히 jsonObject List이기 때문! 그래서 타입추론하게끔 var 사용
            foreach(var el in NPCJsonData[NPC_ID.ToString()]["quest"])
            {
                // NPC_ID에 해당하는 NPC가 가지고 있는 Quest들의 데이터들만 넣어줌
                QuestArray.Add(QuestJsonData[el.ToString()]);
            }

            UI.SetData(NPCJsonData[NPC_ID.ToString()], QuestArray);
        }
    }
}

