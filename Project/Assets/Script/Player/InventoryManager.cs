using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventoryMGR{

    public enum EGetType
    {
        Compensation,
        Drop,
        Shop,
    }

    public class InventoryManager : MonoBehaviour
    {
        Dictionary<int, int> InventoryList;

        void Start()
        {
            
        }

        public void GetItemfrom(int ItemCode, int num, EGetType Type)
        {
            Debug.LogFormat("Get ItemCode : {0} , num : {1}, GetType : {2}"
                            ,ItemCode, num, Type);

            if(!InventoryList.ContainsKey(ItemCode))
            {
                InventoryList.Add(ItemCode, num);
                return;
            }

            InventoryList[ItemCode] = InventoryList[ItemCode] + num;
                
        }

        public void RemoveFromInventory(int ItemCode, int num)
        {
            if(!InventoryList.ContainsKey(ItemCode)) { return; }

            if(InventoryList[ItemCode] == 1)
            {
                InventoryList.Remove(ItemCode);
            }

            else // 여기 로직 중요
            {
                InventoryList[ItemCode] = InventoryList[ItemCode] - num;
            }

            UpdateInventoryUI();

        }

        private void GetAllItem()
        {
            foreach (KeyValuePai<int, int> el in InventoryList)
            {
                Debug.LogFormat("Item : {0}", el.Key);
            }
        }
        
        // important
        public void UpdateInventoryUI()
        {
            GetAllItem();
            // TODO
        }
    }
}

