using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PlayerInfoUI : MonoBehaviour
{
    public static PlayerInfoUI instance;
    public TextMeshProUGUI playerName, level, playerMoney;
    public Image Xp,Hp;
    [SerializeField]
    private Button Button_Exit;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Hp.rectTransform.localScale = new Vector3(PlayerInfo.instance.CurrentHealth / PlayerInfo.instance.MaxHealth, 1f, 1f);
        Xp.rectTransform.localScale = new Vector3(PlayerInfo.instance.nowExp / PlayerInfo.instance.MaxExpByLevel, 1f, 1f);
        Button_Exit.onClick.AddListener(OnClicked_Exit);
    }

    private void Update()
    {
        SetPlayerInfo();

    }

    public void OnClicked_Exit() { gameObject.SetActive(false); }

    public void SetPlayerInfo()
    {
        level.text = PlayerInfo.instance.level.ToString();
        Hp.rectTransform.localScale = new Vector3(PlayerInfo.instance.CurrentHealth / PlayerInfo.instance.MaxHealth, 1f, 1f);
        Xp.rectTransform.localScale = new Vector3(PlayerInfo.instance.nowExp / PlayerInfo.instance.MaxExpByLevel, 1f, 1f);
    }


}
