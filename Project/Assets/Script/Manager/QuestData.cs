using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestData 
{
    public int questIdx;
    public int questType;
    public int fromQuest;
    public int toQuest;
    public int goal0;
    public int goal1;
    public int nowstate0;
    public int nowstate1;
    public string compensation_ItemName;
    public int compensation_ItemID;
    public int compensation_ItemNum;
    public bool iscompleted;
    public bool changeToNext;
    public string title;
    public string desc;
    public string completed_text;

    public QuestData(int questIdx, int questType, int fromQuest, int toQuest, int goal0, int goal1, int nowstate0, int nowstate1, string compensation_ItemName, int compensation_ItemID, int compensation_ItemNum,string title, string desc, string completed_text, bool iscompleted, bool changeToNext)
    {
        this.questIdx = questIdx;
        this.questType = questType;
        this.fromQuest = fromQuest;
        this.toQuest = toQuest;
        this.goal0 = goal0;
        this.goal1 = goal1;
        this.nowstate0 = nowstate0;
        this.nowstate1 = nowstate1;
        this.compensation_ItemName = compensation_ItemName;
        this.compensation_ItemID = compensation_ItemID;
        this.compensation_ItemNum = compensation_ItemNum;
        this.title = title;
        this.desc = desc;
        this.completed_text = completed_text;
        this.iscompleted = iscompleted;
        this.changeToNext = changeToNext;
    }
}


