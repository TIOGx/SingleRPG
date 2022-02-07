using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo instance;
    public float nowExp;
    public float MaxExpByLevel;
    public int level;
    public int[] ExpListByLevel;

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
        if(MaxExpByLevel <= nowExp) // 레벨업 조건이 만족되었을 때
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
        MaxExpByLevel = ExpListByLevel[level];
    }


}
