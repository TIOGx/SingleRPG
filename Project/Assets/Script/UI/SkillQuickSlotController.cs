using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillQuickSlotController : MonoBehaviour
{
    [SerializeField] private Slot[] quickSlots;  // Äü½½·Ôµé (5°³)
    [SerializeField] private Transform tf_parent;  // Äü½½·ÔµéÀÇ ºÎ¸ð ¿ÀºêÁ§Æ®

    private int selectedSlot;  // ¼±ÅÃµÈ Äü½½·ÔÀÇ ÀÎµ¦½º (0~4)


    void Start()
    {
        quickSlots = tf_parent.GetComponentsInChildren<Slot>();
        selectedSlot = 0;
    }

    void Update()
    {
        TryInputNumber();
    }

    private void TryInputNumber()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            ChangeSlot(0);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            ChangeSlot(1);
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            ChangeSlot(2);
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            ChangeSlot(3);
        else if (Input.GetKeyDown(KeyCode.Alpha5))
            ChangeSlot(4);
    }

    private void ChangeSlot(int _num)
    {
        SelectedSlot(_num);
        Debug.Log(_num + "¹ø½½·Ô ¼±ÅÃ");
        // Execute();
    }

    private void SelectedSlot(int _num)
    {
        // ¼±ÅÃµÈ ½½·Ô
        selectedSlot = _num;
    }
    /*
    private void Execute()
    {
        if (quickSlots[selectedSlot].item != null)
        {
            if (quickSlots[selectedSlot].item.itemType == Item.ItemType.Equipment)
                StartCoroutine(theWeaponManager.ChangeWeaponCoroutine(quickSlots[selectedSlot].item.weaponType, quickSlots[selectedSlot].item.itemName));
            else if (quickSlots[selectedSlot].item.itemType == Item.ItemType.Used)
                StartCoroutine(theWeaponManager.ChangeWeaponCoroutine("HAND", "¸Ç¼Õ"));
            else
                StartCoroutine(theWeaponManager.ChangeWeaponCoroutine("HAND", "¸Ç¼Õ"));
        }
        else
        {
            StartCoroutine(theWeaponManager.ChangeWeaponCoroutine("HAND", "¸Ç¼Õ"));
        }
    }
    */
}
