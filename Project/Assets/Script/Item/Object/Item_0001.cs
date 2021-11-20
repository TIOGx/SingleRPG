using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Item;

public class Item_0001 : ItemBase
{
    void Start()
    {
        Type = EItemType.Etc;
        ItemCode = 1;
        bIsDropped = true;
    }
}
