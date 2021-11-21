using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InventoryItemUI
{
    public class UInventoryItemUI : MonoBehaviour
    {
        [SerializeField]
        private Text Text_ItemName, Text_ItemNum;

        public void SetText(string Name, int Num)
        {
            Text_ItemName.GetComponent<Text>().text = Name;
            Text_ItemNum.GetComponent<Text>().text = Num.ToString();
        }
    }
}

