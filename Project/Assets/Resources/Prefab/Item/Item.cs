using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject  // 게임 오브젝트에 붙일 필요 X 
{
    public enum ItemType  // 아이템 유형
    {
        Equipment, // 장비
        Used, // 소모품
        Ingredient, // 재료
        ETC, // 기타
    }

    public string itemName; // 아이템의 이름
    public int itemId; // 아이템의 ID
    public ItemType itemType; // 아이템 유형
    public Sprite itemImage; // 아이템의 이미지(인벤 토리 안에서 띄울) Sprite로 하는 이유는 Canvas가 아니여도 월드 어디서든 이미지를 띄울 수 있기 때문에
    public GameObject itemPrefab;  // 아이템의 프리팹 (아이템 생성시 프리팹으로 찍어냄)
}