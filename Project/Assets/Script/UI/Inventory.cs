using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public static bool invectoryActivated = false;  // ???????? ?????? ????. true?? ???? ?????? ???????? ???? ?????? ???? ??????.

    public TextMeshProUGUI goldText;

    [SerializeField]
    private GameObject go_InventoryBase; // Inventory_Base ??????
    [SerializeField]
    private GameObject go_SlotsParent;  // Slot???? ?????? Grid Setting 

    private Slot[] slots;  // ?????? ????

    [SerializeField]
    private Button InventoryButton;  

    [SerializeField]
    private Button ExitButton;  


    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        slots = go_SlotsParent.GetComponentsInChildren<Slot>();
        InventoryButton.onClick.AddListener(TryOpenInventory);
        ExitButton.onClick.AddListener(CloseInventory);
    }

 
    private void TryOpenInventory()
    {
        invectoryActivated = !invectoryActivated;

        if (invectoryActivated)
            OpenInventory();
        else
            CloseInventory();
    }

    private void OpenInventory()
    {
        go_InventoryBase.SetActive(true);
    }

    private void CloseInventory()
    {
        go_InventoryBase.SetActive(false);
    }
    public bool CheckItem(Item item_)
    {
        for(int i = 0; i< slots.Length; i++)
        {
            if(slots[i].item != null)
            {
                if(slots[i].item.itemName == item_.itemName)
                {
                    return true;
                }
            }
        }
        return false;
    }
    public void UseItem(Item _item, int _count = -1)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)  // null ?????? slots[i].item.itemName ?? ?? ?????? ???? ????
            {
                if (slots[i].item.itemName == _item.itemName)
                {
                    slots[i].SetSlotCount(_count);
                    return;
                }
            }
        }
    }
    public void AcquireItem(Item _item, int _count = 1)
    {
        if (Item.ItemType.Equipment != _item.itemType)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)  // null ?????? slots[i].item.itemName ?? ?? ?????? ???? ????
                {
                    if (slots[i].item.itemName == _item.itemName)
                    {
                        slots[i].SetSlotCount(_count);
                        return;
                    }
                }
                else // 없던 아이템을 먹었을 때
                {
                    slots[i].AddItem(_item, _count);
                    return;
                }
            }
        }
    }

}