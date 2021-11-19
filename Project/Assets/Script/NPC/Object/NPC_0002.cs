using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NPCManager;

public class NPC_0002 : NPCBase
{
    void Start()
    {
        base.Start();
        NPC_ID = 2;
        QuestList = new List<int>{3};
    }

}
