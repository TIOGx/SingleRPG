using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.IO;

public class QuestManager : MonoBehaviour
{
    [SerializeField]
    private GameObject QuestDataUI;
    [SerializeField]
    private Text Text_Title, Text_Desc;
    [SerializeField]
    private Queue<QuestData> questQueue;

    private string jsonString;

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

        questQueue.Enqueue(new QuestData(0, int.Parse(QuestJsonData[0]["type"].ToString()), int.Parse(QuestJsonData[0]["fromQuest"].ToString()), int.Parse(QuestJsonData[0]["toQuest"].ToString()), int.Parse(QuestJsonData[0]["goal"].ToString()), int.Parse(QuestJsonData[0]["compensation_ItemID"].ToString()), int.Parse(QuestJsonData[0]["compensation_ItemNum"].ToString()), QuestJsonData[0]["title"].ToString(), QuestJsonData[0]["desc"].ToString()));
        //Debug.Log(questQueue);
        Text_Title.GetComponent<Text>().text = questQueue.Peek().title;
        Text_Desc.GetComponent<Text>().text = questQueue.Peek().desc;

    }

    private void UpdateQuestUI()
    {
        foreach (var el in questQueue)
        {
            Text_Title.GetComponent<Text>().text = el.title;
            Text_Desc.GetComponent<Text>().text = el.desc;

        }
    }

}
