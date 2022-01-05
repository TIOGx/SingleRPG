using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Item;

public class Item_0002 : ItemBase
{
    void Start()
    {
        Type = EItemType.Cloth;
        ItemCode = 2;
        bIsDropped = true;
    }
}
