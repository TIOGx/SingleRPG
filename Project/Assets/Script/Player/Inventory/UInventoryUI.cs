using System.Collections;
using System.Collections.Generic;
using LitJson;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using InventoryItemUI;

namespace InventoryUI
{
    public class UInventoryUI : MonoBehaviour
    {
        [SerializeField]
        private GameObject UItemUI;
        [SerializeField]
        private Button Button_Exit;
        
        public GameObject ContentBox;

        private UInventoryItemUI ItemUIInstance;

        void Start()
        {
            Button_Exit.onClick.AddListener(EndUI);
        }

        public void Init(Dictionary<int, int> InInventoryList)
        {
            string jsonString = File.ReadAllText(Application.dataPath + "/Data/Item_Data/ItemData.json");
            if(jsonString == "") { return; }
            JsonData ItemJsonData = JsonMapper.ToObject(jsonString);
            
            foreach(var el in InInventoryList)
            {
                ItemUIInstance = Instantiate(UItemUI).GetComponent<UInventoryItemUI>();
                // UItemUI.transform.parent =
                // ContentBox 하위 자식으로 넣기
                ItemUIInstance.SetText(ItemJsonData[el.Key.ToString()].ToString(), el.Value);
            }
            
        }

        void EndUI()
        {
            Destroy(gameObject);
        }
    }
}

