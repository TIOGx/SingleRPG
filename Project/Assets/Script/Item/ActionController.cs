using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ActionController : MonoBehaviour
{
    [SerializeField]
    private float range;  // ?????? ?????? ?????? ???? ????

    private bool pickupActivated = false;  // ?????? ???? ???????? True 
    private bool canPickUp = false;
    private RaycastHit hitInfo;  // ?????? ???? ????

    [SerializeField]
    private LayerMask layerMask;  // ???? ???????? ???? ?????????? ???????? ?????? ?? ?????? ????.

    [SerializeField]
    private Text actionText;  // ?????? ???? ?? ??????

    [SerializeField]
    private Inventory theInventory;  // Inventory.cs

    [SerializeField]
    private Button InventoryButton;  // Inventory.cs


    IEnumerator setPickUpDelay(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        canPickUp = false;
    }

    private void Start()
    {
        InventoryButton.onClick.AddListener(TryAction);
    }

    void Update()
    {
        CheckItem();
        TryAction();
    }

    private void TryAction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!canPickUp)
            {
                canPickUp = true;
                CheckItem();
                CanPickUp();
                StartCoroutine(setPickUpDelay(1.7f));
            }   
        }
    
    }

    private void CheckItem()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, range, layerMask))
        {
            if (hitInfo.transform.tag == "Item")
            {
                ItemInfoAppear();
            }
            else
            {
                ItemInfoDisappear();
            }
        }
        else
        {
            ItemInfoDisappear();
        }

    }

    private void ItemInfoAppear()
    {
        pickupActivated = true;
        actionText.gameObject.SetActive(true);
        actionText.text = hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + " Acquire " + "<color=yellow>" + "(E)" + "</color>";
    }

    private void ItemInfoDisappear()
    {
        pickupActivated = false;
        actionText.gameObject.SetActive(false);
    }

    private void CanPickUp()
    {
        if (pickupActivated)
        {
            if (hitInfo.transform != null)
            {
                Debug.Log(hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + " 아이템을 주울 수 있습니다.");  // ???????? ????
                theInventory.AcquireItem(hitInfo.transform.GetComponent<ItemPickUp>().item);

                if (hitInfo.transform.GetComponent<ItemPickUp>().item.itemId == 10) // Item이 포션일 때
                {
                    Debug.Log("포션 텍스트 업데이틍");
                    PlayerInfo.instance.playerPotionText.text = (int.Parse(PlayerInfo.instance.playerPotionText.text) + int.Parse(1.ToString())).ToString();
                }

                QuestManager.instance.checkQuest(3, hitInfo.transform.GetComponent<ItemPickUp>().item.itemId); 
                ItemDataUI.instance.InstantiateItemDataUI(hitInfo.transform.GetComponent<ItemPickUp>().item.itemName, 1.ToString());

                Destroy(hitInfo.transform.gameObject);
                ItemInfoDisappear();
            }
            else { return; }
        }
        else { return; }
    }
}

