using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject  // ���� ������Ʈ�� ���� �ʿ� X 
{
    public enum ItemType  // ������ ����
    {
        Equipment, // ���
        Used, // �Ҹ�ǰ
        Ingredient, // ���
        ETC, // ��Ÿ
    }

    public string itemName; // �������� �̸�
    public int itemId; // �������� ID
    public ItemType itemType; // ������ ����
    public Sprite itemImage; // �������� �̹���(�κ� �丮 �ȿ��� ���) Sprite�� �ϴ� ������ Canvas�� �ƴϿ��� ���� ��𼭵� �̹����� ��� �� �ֱ� ������
    public GameObject itemPrefab;  // �������� ������ (������ ������ ���������� ��)
}