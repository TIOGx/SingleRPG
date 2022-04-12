 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo instance;

    public TextMeshProUGUI levelText;
    public TextMeshProUGUI levelupCanvasText;
    public TextMeshProUGUI playerMoneyText;
    public TextMeshProUGUI playerPotionText;
    public float nowExp;
    public float MaxExpByLevel;
    public int level;
    public int[] ExpListByLevel;
    public float CurrentHealth;
    public float MaxHealth;
    public GameObject bossHp;
    public GameObject levelupCanvas;
    public GameObject playerinfoCanvas;
    public GameObject playerDieCanvas;
    public GameObject informationCanvas;
    public GameObject shopCanvas;
    public GameObject gameclearCanvas;

    public float AttackDamage;
    public float MagicDamage;

    private void Awake()
    {
        instance = this;
        level = 1;
        nowExp = 0;
        MaxExpByLevel = ExpListByLevel[level];
    }

    public void playerinfo_True() { playerinfoCanvas.SetActive(true); }
    public void playerinfo_False() { playerinfoCanvas.SetActive(false); }

    public void information_True() { informationCanvas.SetActive(true); }
    public void information_False() { informationCanvas.SetActive(false); }

    public void shop_True() { shopCanvas.SetActive(true); }
    public void shop_False() { shopCanvas.SetActive(false); }

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

    void IncreasePlayerStatByLevel(int level)
    {
        AttackDamage += 5 ;
        MagicDamage +=  5 ;
        MaxHealth += level * 10;
        CurrentHealth = MaxHealth;
    }

    public void PlayerLevelUp()
    {
        nowExp = nowExp - MaxExpByLevel;
        level += 1;
        IncreasePlayerStatByLevel(level);
        UserInterface.instance.UpdateHpBarUI(CurrentHealth / MaxHealth);
        UserInterface.instance.NowHp.text = CurrentHealth.ToString();
        UserInterface.instance.MaxHp.text = MaxHealth.ToString();
        levelupCanvasText.text = level.ToString();
        levelupCanvas.SetActive(true);
        Pause.Instance.TimePause();
    }

    public void OnClicked_Confirm()
    {
        levelText.text = level.ToString();
        MaxExpByLevel = ExpListByLevel[level];
        levelupCanvas.SetActive(false);
        Pause.Instance.TimePause();
    }

    public void OnClicked_YDieCanvas()
    {
        Respawn();
        playerDieCanvas.SetActive(false);
    }

    public void Respawn()
    {
        CurrentHealth = MaxHealth;
        UserInterface.instance.NowHp.text = CurrentHealth.ToString();
        UserInterface.instance.UpdateHpBarUI(1);
        gameObject.transform.position = new Vector3(0f, 0f, -10f);
        Pause.Instance.TimePause();
    }

    public void GoToBoss()
    {
        gameObject.transform.position = new Vector3(1100f, 0f, -10f);
        bossHp.SetActive(true);
    }

    public void HealPlayer(float amount)
    {
        CurrentHealth += amount;
        if(CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
    }

    public void OnClicked_NDieCanvas() // 로비 씬으로 돌아가기
    {

    }



}
