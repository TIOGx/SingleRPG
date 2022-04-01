using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillQuickSlotController : MonoBehaviour
{
    [SerializeField] private Slot[] quickSlots;  // �����Ե� (5��)
    [SerializeField] private Transform tf_parent;  // �����Ե��� �θ� ������Ʈ

    private int selectedSlot;  // ���õ� �������� �ε��� (0~4)


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
        Debug.Log(_num + "������ ����");
        // Execute();
    }

    private void SelectedSlot(int _num)
    {
        // ���õ� ����
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
                StartCoroutine(theWeaponManager.ChangeWeaponCoroutine("HAND", "�Ǽ�"));
            else
                StartCoroutine(theWeaponManager.ChangeWeaponCoroutine("HAND", "�Ǽ�"));
        }
        else
        {
            StartCoroutine(theWeaponManager.ChangeWeaponCoroutine("HAND", "�Ǽ�"));
        }
    }
    */
}
