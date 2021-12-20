using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InventoryUI;

namespace InventoryMGR{

    public enum EGetType
    {
        Compensation,
        Drop,
        Shop,
    }

    public class InventoryManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject InventoryUI; // 인벤토리 UI프리팹

        private UInventoryUI UI;

        [System.Serializable]
        struct ItemData
        {
            int ItemCode;
            int Num;

            public ItemData(int _c, int _n)
            {
                this.ItemCode = _c;
                this.Num = _n;
            }
        }

        // 딕셔너리 형식 <ItemCode : NumOfItem>
        Dictionary<int, int> InventoryList;

        void Start()
        {
            InventoryList = new Dictionary<int, int>();
        }

        void Update()
        {
            // 인풋 저번에 내가 카톡에 올린 Bind, Delegate로 변경하기! 아래는 테스트
            if(Input.GetKeyDown(KeyCode.I))
            {
                InitUI();                
            }
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
            SaveData();
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
                Debug.LogFormat("Item : {0}, Count = {1}", el.Key, el.Value);
            }
        }
        
        // important
        public void UpdateInventoryUI()
        {
            GetAllItem();
            // TODO
        }

        private void SaveData()
        {
            Debug.Log("File try save");
            FileSystem.Save<ItemData>(new ItemData(1,1));
        }

        public void InitUI()
        {
            UI = Instantiate(InventoryUI).GetComponent<UInventoryUI>();
            if(UI == null) { return; }

            UI.Init(InventoryList);
        }

        public void EndUI()
        {
            Destroy(InventoryUI);
        }
    }
}
