using System;
using System.Collections.Generic;
using UnityEngine;

public class SkillpoolManager : MonoBehaviour
{
    public static SkillpoolManager Instance;
    public List<GameObject> SkillPrefabList = new List<GameObject>();

    public Dictionary<KeyCode, GameObject> KeyDictionary;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {

        KeyDictionary = new Dictionary<KeyCode, GameObject>()
        {
            { KeyCode.Z, SkillPrefabList[0] },
            { KeyCode.X, SkillPrefabList[1] }
        };
        
    }
    private void Update()
    {
        GetKeyInput();
    }

    private void GetKeyInput()
    {
        if (Input.anyKeyDown)
        {
            foreach (var dic in KeyDictionary)
            {
                if (Input.GetKeyDown(dic.Key)) // ��ų�� ������� Ȯ��
                {
                    Debug.Log(KeyDictionary[dic.Key]);
                    // ��ų ���� ���� üũ �ؾߴ� (����, ��Ÿ�� ���)
                    if(KeyDictionary[dic.Key].GetComponent<Skill>().CanUseSkill == true)
                    {
                        KeyDictionary[dic.Key].GetComponent<Skill>().UseSkill();
                    }
                    else 
                    {
                        Debug.Log(KeyDictionary[dic.Key].GetComponent<Skill>().Skillname);
                        Debug.Log(KeyDictionary[dic.Key].GetComponent<Skill>().CanUseSkill);
                        Debug.Log("��ų ���� ���� ������");
                    }
                    
                }
            }
        }
    }
}
