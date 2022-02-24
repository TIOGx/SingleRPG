using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PlayerInfoUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI  playerName, level, playerMoney;
    [SerializeField]
    private Button Button_Exit;

    private void Start()
    {
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
    }


}
