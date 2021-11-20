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
        private EItemType Type;
        private int ItemCode;
        private bool bIsDropped;

        public ItemBase(EItemType _type, int _itemCode, bool _bIsDropped)
        {
            this.Type = _type;
            this.ItemCode = _itemCode;
            this.bIsDropped = _bIsDropped;
        }

        void SendToPlayerInventory(int InItemCode)
        {
            GameObject.FindWithTag("Player").GetComponent<InventoryManager>().GetItemfrom(InItemCode, 1, EGetType.Drop);
        }

        private void OnCollisionEnter(Collision other) {
            if(other.gameObject.tag != "Player") { return; }
            SendToPlayerInventory(this.ItemCode);
        }
    }
}