using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPCManager{
    // NPC관련한 모든 interface를 여기에 작성

    // for NPC class
    public interface INPC {
        List<int> QuestList {get; set;}
        GameObject NPCUI {get; set;}

        void StartQuestUI(int Q_ID);
        void Accept(int Q_ID);
        void Reject();
        void GivePlayerCompensation(); //보상주
    }

    // for NPC UI class
    public interface INPCUI {
        void SetText(UNPCUIText InData);
        void OnClicked_Exit();
        void OnClicked_Accept();
        void OnClicked_Reject();
    }
    public struct UNPCUIText
    {
        public string Title, Desc; /*..etc*/
        
        public UNPCUIText(string T, string D)
        {
            this.Title = T;
            this.Desc = D;
        }
    }


    public class NPC_Manager : MonoBehaviour, INPC
    {
        private List<int> _QuestList;
        private GameObject _NPCUI;

        public List<int> QuestList
        {
            get => _QuestList;
            set => _QuestList = value;
        }
        public GameObject NPCUI {
            get => _NPCUI;
            set => _NPCUI = value;
        }

        public void StartQuestUI(int Q_ID)
        {
            // Instantiate();
        }

        public void Accept(int Q_ID)
        {
            Debug.Log("ACCEPT");
        }

        public void Reject()
        {
            Debug.Log("Reject");
        }

        public void GivePlayerCompensation() //보상주기
        {
            Debug.Log("COMPENSATION");
        }

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}

