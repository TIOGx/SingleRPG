using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestData 
{
    public int questIdx;
    public int questType;
    public int fromQuest;
    public int toQuest;
    public int goal;
    public int compensation_ItemID;
    public int compensation_ItemNum;
    public string title;
    public string desc;

    public QuestData(int questIdx, int questType, int fromQuest, int toQuest, int goal, int compensation_ItemID, int compensation_ItemNum, string title, string desc)
    {
        this.questIdx = questIdx;
        this.questType = questType;
        this.fromQuest = fromQuest;
        this.toQuest = toQuest;
        this.goal = goal;
        this.compensation_ItemID = compensation_ItemID;
        this.compensation_ItemNum = compensation_ItemNum;
        this.title = title;
        this.desc = desc;
    }
}


