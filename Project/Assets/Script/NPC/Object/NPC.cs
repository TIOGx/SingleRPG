using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NPCManager; // 상속위해 필수

public class NPC : NPCBase
{
    //public int npc_ID = 1;
    public string npc_Name;

    void Start()
    {
        base.Start(); // 부모의 Start를 먼저 호출해서 QuestUI 로드하기
        NPC_ID = 1;
    }

}
