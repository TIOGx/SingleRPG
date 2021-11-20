using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InventoryMGR;

namespace Item
{
    public enum EItemType
    {
        Epic    = 1,
        Cloth   = 2,
        Waste   = 3,
        Etc     = 4,
    }

    public class ItemBase : MonoBehaviour
    {
        protected EItemType Type;
        protected int ItemCode;
        protected bool bIsDropped;

        int GetItemCode() { return ItemCode; }
        EItemType GetItemType() { return Type; }
        bool GetbIsDropped() { return bIsDropped; }

        void SendToPlayerInventory(int InItemCode)
        {
            GameObject.FindWithTag("Player").GetComponent<InventoryManager>().GetItemfrom(InItemCode, 1, EGetType.Drop);
        }

        protected void OnCollisionEnter(Collision other) {
            if(other.gameObject.tag != "Player") { return; }
            SendToPlayerInventory(this.ItemCode);
            Destroy(gameObject);
        }
    }
}