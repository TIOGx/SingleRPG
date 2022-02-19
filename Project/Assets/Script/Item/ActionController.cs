using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour
{
    [SerializeField]
    private float range;  // ?????? ?????? ?????? ???? ????

    private bool pickupActivated = false;  // ?????? ???? ???????? True 

    private RaycastHit hitInfo;  // ?????? ???? ????

    [SerializeField]
    private LayerMask layerMask;  // ???? ???????? ???? ?????????? ???????? ?????? ?? ?????? ????.

    [SerializeField]
    private Text actionText;  // ?????? ???? ?? ??????

    [SerializeField]
    private Inventory theInventory;  // Inventory.cs

    [SerializeField]
    private Button InventoryButton;  // Inventory.cs

    
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
            CheckItem();
            CanPickUp();
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
        
    }

    private void ItemInfoAppear()
    {
        pickupActivated = true;
        actionText.gameObject.SetActive(true);
        actionText.text = hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + " 획득 " + "<color=yellow>" + "(E)" + "</color>";
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
                ItemDataUI.instance.InstantiateItemDataUI(hitInfo.transform.GetComponent<ItemPickUp>().item.itemName, 1.ToString());
                Destroy(hitInfo.transform.gameObject);
                ItemInfoDisappear();
            }
            else { return; }
        }
        else { return; }
    }
}

