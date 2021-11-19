using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NPCManager; // 상속위해 필수

public class NPC_0001 : NPCBase
{
    void Start()
    {
        base.Start(); // 부모의 Start를 먼저 호출해서 QuestUI 로드하기
        NPC_ID = 1;
        QuestList = new List<int>{1,2};
    }

}
