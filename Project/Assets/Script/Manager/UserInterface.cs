using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
    public static UserInterface instance;
    public Slider ExpBar;
    public Slider HpBar;
    public Text MaxHp;
    public Text NowHp;


    private void Awake()
    {
        instance = this;
        NowHp.text = PlayerInfo.instance.CurrentHealth.ToString();
        MaxHp.text = PlayerInfo.instance.MaxHealth.ToString();
        UpdateHpBarUI(PlayerInfo.instance.CurrentHealth / PlayerInfo.instance.MaxHealth);
    }

    public void UpdateExpBarUI(float value)
    {
        ExpBar.value = value;
    }
    public void UpdateHpBarUI(float value)
    {
        HpBar.value = value;
    }
}
