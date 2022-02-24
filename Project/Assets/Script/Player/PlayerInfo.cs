 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo instance;

    public TextMeshProUGUI levelText;
    public float nowExp;
    public float MaxExpByLevel;
    public int level;
    public int[] ExpListByLevel;

    public float CurrentHealth;
    public float MaxHealth;
    public GameObject levelupCanvas;
    public GameObject playerinfoCanvas;

    private void Awake()
    {
        instance = this;
        level = 1;
        nowExp = 0;
        MaxExpByLevel = ExpListByLevel[level];
    }

    public void playerinfo_True() { playerinfoCanvas.SetActive(true); }
    public void playerinfo_False() { playerinfoCanvas.SetActive(false); }

    public void GetExp(int value)
    {
        nowExp += value;
        if(MaxExpByLevel <= nowExp) 
        {
            UserInterface.instance.UpdateExpBarUI(1);
            PlayerLevelUp();
        }
        UserInterface.instance.UpdateExpBarUI(nowExp/ MaxExpByLevel);
    }

    public void PlayerLevelUp()
    {
        nowExp = nowExp - MaxExpByLevel;
        levelupCanvas.SetActive(true);
        level += 1;
        
    }

    public void OnClicked_Confirm()
    {
        
        levelText.text = level.ToString();
        MaxExpByLevel = ExpListByLevel[level];
        levelupCanvas.SetActive(false);
    }



}
