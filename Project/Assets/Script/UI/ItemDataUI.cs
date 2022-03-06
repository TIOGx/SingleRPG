using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDataUI : MonoBehaviour
{
    public static ItemDataUI instance;
    [SerializeField]
    private GameObject itemDataUI;

    private void Awake()
    {
        instance = this;
    }

    IEnumerator setcompensationDelay(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        ItemData.instance.EndItemDataUI();
    }

    public void InstantiateItemDataUI(string ItemName, string ItemNum)
    {
        var index = Instantiate(itemDataUI);
        index.transform.SetParent(GameObject.Find("ItemDataContent").transform);

        ItemData.instance.ItemName.text = ItemName;
        ItemData.instance.ItemNum.text = ItemNum;

        StartCoroutine(setcompensationDelay(1.5f));

    }
}   
