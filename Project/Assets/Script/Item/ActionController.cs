using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ActionController : MonoBehaviour
{
    [SerializeField]
    private float range;  // ?????? ?????? ?????? ???? ????

    private bool pickupActivated = false;  // ?????? ???? ???????? True
    private bool gotobossActivated = false;
    private bool canPickUp = false;
    private RaycastHit hitInfo;  // ?????? ???? ????

    [SerializeField]
    private LayerMask layerMask;  // ???? ???????? ???? ?????????? ???????? ?????? ?? ?????? ????.

    [SerializeField]
    private Text actionText;  // ?????? ???? ?? ??????

    [SerializeField]
    private Text ToBossText;  // ?????? ???? ?? ??????

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

        else if (Input.GetKeyDown(KeyCode.Y))
        {
            CheckItem();
            CanGoToBoss();
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
            else if (hitInfo.transform.tag == "ToBoss")
            {
                Debug.Log("보스한테 가자고@!!");
                if (QuestManager.instance.cangotoboss) { GoToBossInfoAppear(); }
            }

            else
            {
                ItemInfoDisappear();
                GoToBossInfoDisappear();
            }
        }
        else
        {
            GoToBossInfoDisappear();
            ItemInfoDisappear();
        }

    }


    private void GoToBossInfoAppear()
    {
        gotobossActivated = true;
        ToBossText.gameObject.SetActive(true);
        ToBossText.text = " 보스에게 가기 " + "<color=red>" + "(Y)" + "</color>";
    }

    private void GoToBossInfoDisappear()
    {
        gotobossActivated = false;
        ToBossText.gameObject.SetActive(false);
    }

    private void ItemInfoAppear()
    {
        pickupActivated = true;
        actionText.gameObject.SetActive(true);
        actionText.text = hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + " 얻기 " + "<color=yellow>" + "(E)" + "</color>";
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

    private void CanGoToBoss()
    {
        if (gotobossActivated)
        {
            if (hitInfo.transform != null)
            {
                Debug.Log(" 보스에게 갈 수 있습니다.");
                PlayerInfo.instance.GoToBoss();
            }
            else { return; }
        }
        else { return; }
    }
}

