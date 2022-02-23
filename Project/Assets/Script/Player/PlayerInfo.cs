using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo instance;
    public float nowExp;
    public float MaxExpByLevel;
    public int level;
    public int[] ExpListByLevel;
    public float CurrentHealth;
    public float MaxHealth;
    [SerializeField]
    private Text levelText;

    private void Awake()
    {
        instance = this;
        level = 0;
        nowExp = 0;
        MaxExpByLevel = ExpListByLevel[level];
    }
    public void GetExp(int value)
    {
        nowExp += value;
        if(MaxExpByLevel <= nowExp) // ?????? ?????? ?????????? ??
        {
            UserInterface.instance.UpdateExpBarUI(1);
            PlayerLevelUp();
        }
        UserInterface.instance.UpdateExpBarUI(nowExp/ MaxExpByLevel);
    }

    public void PlayerLevelUp()
    {
        nowExp = nowExp - MaxExpByLevel;
        level += 1;
        Debug.Log("?? ??? ?");
        levelText.text = level.ToString();
        MaxExpByLevel = ExpListByLevel[level];
    }


}
