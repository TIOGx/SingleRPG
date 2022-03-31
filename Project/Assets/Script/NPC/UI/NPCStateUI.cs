using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCStateUI : MonoBehaviour
{
    public static NPCStateUI instance;
    [SerializeField]
    private GameObject Qmark;
    [SerializeField]
    private GameObject Emark;

    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        CheckQuestStateUI();
    }

    // 퀘스트를 받을 수 잇는 상태, ? 표시
    public void CanProgressQuest(bool tf) { Qmark.SetActive(tf); }
    // 보상을 받을 수 잇는 상태, ! 표시
    public void CanGetcompensationQuest(bool tf) { Emark.SetActive(tf); }

    private void CheckQuestStateUI()
    {
        // 퀘스트를 받을 수 있는 상태
        if (QuestManager.instance.getQuestQueue().Peek().fromQuest == gameObject.GetComponent<NPC>().NPC_ID)
        {
            CanProgressQuest(true);
            // 보상을 받을 수 잇는 상태
            if (QuestManager.instance.getQuestQueue().Peek().goal1 == QuestManager.instance.getQuestQueue().Peek().nowstate1)
            {
                CanProgressQuest(false);
                CanGetcompensationQuest(true);
            }
            else
            {
                CanGetcompensationQuest(false);
            }
        }
        // 퀘스트를 받을 수 없는 상태
        else
        {
            CanProgressQuest(false);
            CanGetcompensationQuest(false);
        }
    }

}
