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
                if (Input.GetKeyDown(dic.Key)) // 스킬을 배웠는지 확인
                {
                    Debug.Log(KeyDictionary[dic.Key]);
                    // 스킬 시전 조건 체크 해야댐 (마나, 쿨타임 등등)
                    if(KeyDictionary[dic.Key].GetComponent<Skill>().CanUseSkill == true)
                    {
                        KeyDictionary[dic.Key].GetComponent<Skill>().UseSkill();
                    }
                    else 
                    {
                        Debug.Log(KeyDictionary[dic.Key].GetComponent<Skill>().Skillname);
                        Debug.Log(KeyDictionary[dic.Key].GetComponent<Skill>().CanUseSkill);
                        Debug.Log("스킬 시전 조건 미충족");
                    }
                    
                }
            }
        }
    }
}
