using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCActionController : MonoBehaviour
{
    [SerializeField]
    private float range;  // 아이템 습득이 가능한 최대 거리

    private bool talkActivated = false;

    private RaycastHit hitInfo; // 충돌체 정보 저장

    [SerializeField]
    private LayerMask layerMask;  // 특정 레이어를 가진 오브젝트에 대해서만 습득할 수 있어야 한다.

    [SerializeField]
    private Text actionText; // 행동을 보여 줄 텍스트

    void Update()
    {
        CheckNPC();
        TryAction();
    }

    private void TryAction()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            CheckNPC();
            CanTalk();
        }
    }

    private void CheckNPC()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, range, layerMask))
        {
            if (hitInfo.transform.tag == "Npc")
            {
                NPCInfoAppear();
            }
            else
            {
                NPCInfoDisappear();
            }

        }

    }

    private void NPCInfoAppear()
    {
        talkActivated = true;
        actionText.gameObject.SetActive(true);
        actionText.text = hitInfo.transform.GetComponent<NPC>().npc_Name + " 와 대화하기 " + "<color=blue>" + "(T)" + "</color>";
    }

    private void NPCInfoDisappear()
    {
        talkActivated = false;
        actionText.gameObject.SetActive(false);
    }

    private void CanTalk()
    {
        if (talkActivated)
        {
            if (hitInfo.transform != null)
            {
                Debug.Log(hitInfo.transform.GetComponent<NPC>().npc_Name + "와 상호작용 할 수 있습니다.");
                QuestManager.instance.checkQuest(2, hitInfo.transform.GetComponent<NPC>().NPC_ID);
                NPCManager.NPCBase.instance.InitUI(hitInfo.transform.GetComponent<NPC>().NPC_ID); 
                NPCInfoDisappear();
            }
            else { return; }
        }
        else { return ; }
    }
}

