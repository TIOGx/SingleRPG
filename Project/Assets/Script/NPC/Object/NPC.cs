using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NPCManager; // 상속위해 필수

public class NPC : NPCBase
{
    public string npc_Name;

    [SerializeField]
    private GameObject Qmark;
    [SerializeField]
    private GameObject Emark;

    void Start()
    {
        base.Start(); // 부모의 Start를 먼저 호출해서 QuestUI 로드하기
    }

    // 퀘스트를 받을 수 잇는 상태, ? 표시
    public void CanProgressQuest(bool tf) { Qmark.SetActive(tf); }
    // 보상을 받을 수 잇는 상태, ! 표시
    public void CanGetcompensationQuest(bool tf) { Emark.SetActive(tf); }

}

