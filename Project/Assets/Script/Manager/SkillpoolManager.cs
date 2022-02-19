using System;
using System.Collections.Generic;
using UnityEngine;

public class SkillpoolManager : MonoBehaviour
{
    public static SkillpoolManager Instance;

    [SerializeField]
    private GameObject poolingObjectPrefab;
    public List<GameObject> SkillPrefabList = new List<GameObject>();

    public Dictionary<KeyCode, Skill> KeyDictionary;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        KeyDictionary = new Dictionary<KeyCode, Skill>
        {
            { KeyCode.Z, new Skill ("fireball")},
            { KeyCode.X, new Skill ("Iceball") }
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
                if (Input.GetKeyDown(dic.Key))
                {
                    KeyDictionary[dic.Key].UseSkill();
                }
            }
        }
    }

}
