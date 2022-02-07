using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemData : MonoBehaviour
{
    public static ItemData instance;
    [SerializeField]
    private Text ItemNameTx;
    [SerializeField]
    private Text ItemNumTx;

    private void Awake()
    {
        instance = this;
    }

    public Text ItemName
    {
        get => ItemNameTx; 
        set => ItemNameTx = value;
    }

    public Text ItemNum
    {
        get => ItemNumTx;
        set => ItemNumTx = value;
    }

    public void EndItemDataUI()
    {
        Destroy(gameObject); 
    }
}
