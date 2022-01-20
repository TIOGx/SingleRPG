using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.IO;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;
    [SerializeField]
    private GameObject QuestDataUI;
    [SerializeField]
    private Text Text_Title, Text_Desc;
    [SerializeField]
    private Queue<QuestData> questQueue;

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

        questQueue.Enqueue(new QuestData(0, int.Parse(QuestJsonData[0]["type"].ToString()), int.Parse(QuestJsonData[0]["fromQuest"].ToString()), int.Parse(QuestJsonData[0]["toQuest"].ToString()), int.Parse(QuestJsonData[0]["goal"][0].ToString()), int.Parse(QuestJsonData[0]["goal"][1].ToString()), int.Parse(QuestJsonData[0]["nowstate"][0].ToString()), int.Parse(QuestJsonData[0]["nowstate"][1].ToString()), int.Parse(QuestJsonData[0]["compensation_ItemID"].ToString()), int.Parse(QuestJsonData[0]["compensation_ItemNum"].ToString()), QuestJsonData[0]["title"].ToString(), QuestJsonData[0]["desc"].ToString()));
        //Debug.Log(questQueue);
        Text_Title.GetComponent<Text>().text = questQueue.Peek().title;
        Text_Desc.GetComponent<Text>().text = questQueue.Peek().desc;

    }

    private void UpdateQuestUI()
    {
        Debug.Log("다음퀘스트UI로 업데이트 하자");
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
        Debug.Log("퀘스트 확인해보자");
        Debug.Log("퀘스트 타입은");
        Debug.Log(QType);
        foreach (var quest in questQueue){
            if (quest.questType == QType){
                Debug.Log("퀘스트 타입 일치");
                Debug.Log(quest.goal0);
                if (quest.goal0 == value) // 몬스터 타입 일치할까?
                {
                    Debug.Log("몬스터 타입 일치");
                    quest.nowstate1 += 1;
                    if (IsComplete(quest)){
                        Debug.Log("퀘스트 클리어");
                        ChangeToNextQuest(quest.questIdx);
                        break;
                    }
                }
                else
                {
                    Debug.Log("몬스터 타입 불일치");
                    Debug.Log(quest.questType);
                    Debug.Log(value);
                }
            }
        }
    }
    public bool IsComplete(QuestData quest)
    {
        Debug.Log("목표 마리수");
        Debug.Log(quest.goal1);
        Debug.Log("내가 잡은 마리수");
        Debug.Log(quest.nowstate1);
        if (quest.goal1 == quest.nowstate1)
        {
            Debug.Log("퀘스트 조건 충족");
            return true;
        }
        Debug.Log("퀘스트 조건 미 충족");
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

        Debug.Log("다음퀘스트로 업데이트 하자");
        questQueue.Enqueue(new QuestData(nextQidx, int.Parse(QuestJsonData[nextQidx]["type"].ToString()), int.Parse(QuestJsonData[nextQidx]["fromQuest"].ToString()), int.Parse(QuestJsonData[nextQidx]["toQuest"].ToString()), int.Parse(QuestJsonData[nextQidx]["goal"][0].ToString()), int.Parse(QuestJsonData[nextQidx]["goal"][1].ToString()), int.Parse(QuestJsonData[nextQidx]["nowstate"][0].ToString()), int.Parse(QuestJsonData[nextQidx]["nowstate"][1].ToString()), int.Parse(QuestJsonData[nextQidx]["compensation_ItemID"].ToString()), int.Parse(QuestJsonData[nextQidx]["compensation_ItemNum"].ToString()), QuestJsonData[nextQidx]["title"].ToString(), QuestJsonData[nextQidx]["desc"].ToString()));
        UpdateQuestUI();
    }
}
