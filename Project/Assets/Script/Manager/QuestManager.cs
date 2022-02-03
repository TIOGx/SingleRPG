using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.IO;
using System;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;
    [SerializeField]
    private GameObject QuestDataUI;
    [SerializeField]
    private Text Text_Title, Text_Desc;
    [SerializeField]
    private Queue<QuestData> questQueue;

    public int nowCompleteIdx ;
    public int nextIdx = 0;
    public GameObject[] compensationItemArr = new GameObject[5];

    private string jsonString;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        questQueue = new Queue<QuestData>();
        Init();
    }

    private void Init()
    {
        if (QuestDataUI == null) { return; }

        jsonString = File.ReadAllText(Application.dataPath + "/Data/Quest_Data/QuestData.json");
        if (jsonString == "") { return; }
        JsonData QuestJsonData = JsonMapper.ToObject(jsonString);

        jsonString = File.ReadAllText(Application.dataPath + "/Data/NPC_Data/NPCData.json");
        if (jsonString == "") { return; }
        JsonData NPCJsonData = JsonMapper.ToObject(jsonString);

        questQueue.Enqueue(new QuestData(0, int.Parse(QuestJsonData[0]["type"].ToString()), int.Parse(QuestJsonData[0]["fromQuest"].ToString()), int.Parse(QuestJsonData[0]["toQuest"].ToString()), int.Parse(QuestJsonData[0]["goal"][0].ToString()), int.Parse(QuestJsonData[0]["goal"][1].ToString()), int.Parse(QuestJsonData[0]["nowstate"][0].ToString()), int.Parse(QuestJsonData[0]["nowstate"][1].ToString()), int.Parse(QuestJsonData[0]["compensation_ItemID"].ToString()), int.Parse(QuestJsonData[0]["compensation_ItemNum"].ToString()), QuestJsonData[0]["title"].ToString(), QuestJsonData[0]["desc"].ToString(), QuestJsonData[0]["completed_text"].ToString(), bool.Parse(QuestJsonData[0]["isCompleted"].ToString())));
    }

    public void UpdateQuestUI()
    {
        Debug.Log("UI 다음 퀘스트 내용으로 업데이트");
        foreach (var el in questQueue)
        {
            Debug.Log(el.title);
            Debug.Log(el.desc);
            Text_Title.GetComponent<Text>().text = el.title;
            Text_Desc.GetComponent<Text>().text = el.desc;

        }
    }
    public void checkQuest(int QType, int value)
    {
        if (questQueue == null) { return; }
        Debug.Log("들어온 퀘스트 타입" + QType);
        foreach (var quest in questQueue)
        {
            if (quest.questType == QType)
            {
                Debug.Log("퀘스트 큐 안의 퀘스트타입" + quest.questType);
                if (quest.goal0 == value) // 퀘스트 타입 1일때_ 사냥 퀘스트
                {
                    quest.nowstate1 += 1;
                    if (IsComplete(quest))
                    {
                        Debug.Log(" 퀘스트 완료 조건 충족");
                        Item getItem = compensationItemArr[quest.compensation_ItemID].transform.GetComponent<ItemPickUp>().item;
                        Inventory.instance.AcquireItem(getItem, quest.compensation_ItemNum);
                        nowCompleteIdx = quest.questIdx;
                        
                        break;
                    }
                }
            }
        }
    }

    public bool IsComplete(QuestData quest)
    {
        Debug.Log("퀘스트 완료 조건 확인하기");
        Debug.Log(quest.goal1);
        Debug.Log(quest.nowstate1);
        if (quest.goal1 == quest.nowstate1)
        {
            quest.iscompleted = true;
            Debug.Log("퀘스트 iscomplete = " + quest.iscompleted);
            return true;
        }
        return false;
    }


    public void ChangeToNextQuest(int Qidx)
    {
        if (questQueue == null) { return; }
        questQueue.Dequeue();
        int nextQidx = Qidx + 1;

        if (QuestDataUI == null) { return; }
        jsonString = File.ReadAllText(Application.dataPath + "/Data/Quest_Data/QuestData.json");
        if (jsonString == "") { return; }
        JsonData QuestJsonData = JsonMapper.ToObject(jsonString);

        questQueue.Enqueue(new QuestData(nextQidx, int.Parse(QuestJsonData[nextQidx]["type"].ToString()), int.Parse(QuestJsonData[nextQidx]["fromQuest"].ToString()), int.Parse(QuestJsonData[nextQidx]["toQuest"].ToString()), int.Parse(QuestJsonData[nextQidx]["goal"][0].ToString()), int.Parse(QuestJsonData[nextQidx]["goal"][1].ToString()), int.Parse(QuestJsonData[nextQidx]["nowstate"][0].ToString()), int.Parse(QuestJsonData[nextQidx]["nowstate"][1].ToString()), int.Parse(QuestJsonData[nextQidx]["compensation_ItemID"].ToString()), int.Parse(QuestJsonData[nextQidx]["compensation_ItemNum"].ToString()), QuestJsonData[nextQidx]["title"].ToString(), QuestJsonData[nextQidx]["desc"].ToString(), QuestJsonData[nextQidx]["completed_text"].ToString(), bool.Parse(QuestJsonData[nextQidx]["isCompleted"].ToString())));
        
    }


    public Queue<QuestData> getQuestQueue()
    {
        if(questQueue.Count == 0) { return null; }
        return questQueue;
    }

}
