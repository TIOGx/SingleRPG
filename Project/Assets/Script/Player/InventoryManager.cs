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
            InventoryList = new Dictionary<int, int>();
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
            
            UpdateInventoryUI();
        }

        public void RemoveFromInventory(int ItemCode, int num)
        {
            if(!InventoryList.ContainsKey(ItemCode)) { return; }

            if(InventoryList[ItemCode] - num < 1)
            {
                InventoryList.Remove(ItemCode);
            }

            else // 여기 로직 좀더 세분화 필요
            {
                InventoryList[ItemCode] = InventoryList[ItemCode] - num;
            }

            UpdateInventoryUI();
        }

        private void GetAllItem()
        {
            foreach (KeyValuePair<int, int> el in InventoryList)
            {
                Debug.LogFormat("Item : {0}, Count = {1}", el.Key, el.Value);            }
        }
        
        // important
        public void UpdateInventoryUI()
        {
            GetAllItem();
            // TODO
        }
    }
}

