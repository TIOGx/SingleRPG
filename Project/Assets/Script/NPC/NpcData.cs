using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcData 
{
    public int npcIdx;
    public string name;
    public int fromNpc; 
    public string title;
    public string desc;
    public string completed_text;
    public int quest_prev;
    public int quest_next;
    public int isCompleted;

    public NpcData(int npcIdx ,string name, int fromNpc, string title, string desc, string completed_text, int quest_prev, int quest_next, int isCompleted)
    {
        this.npcIdx = npcIdx;
        this.name = name;
        this.fromNpc = fromNpc;
        this.title = title;
        this.desc = desc;
        this.completed_text = completed_text;
        this.quest_next = quest_next;
        this.quest_prev = quest_prev;
        this.isCompleted = isCompleted;
    }
}
