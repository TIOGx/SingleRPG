using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreManager : MonoBehaviour
{
    public TextMeshProUGUI StoreMoneyText;
    public GameObject NotEnoughMoneyCanvas;
    public GameObject Pitem;
    private bool NotEnoughMoneyDelay = false;

    private void Update()
    {
        StoreMoneyText.text = PlayerInfo.instance.playerMoneyText.text;
    }

    IEnumerator setNotEnoughMoneyDelay(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        NotEnoughMoneyCanvas.SetActive(false);
        NotEnoughMoneyDelay = false;
    }

    public void BuyPotion()
    {
        if(int.Parse(PlayerInfo.instance.playerMoneyText.text) < 10)
        {
            Debug.Log("돈 부족");
            if (!NotEnoughMoneyDelay) {
                NotEnoughMoneyDelay = true;
                NotEnoughMoneyCanvas.SetActive(true);
                StartCoroutine(setNotEnoughMoneyDelay(3.0f));
            }
        }
        else
        {
            Inventory.instance.AcquireItem(Pitem.GetComponent<ItemPickUp>().item);

            PlayerInfo.instance.playerMoneyText.text = (int.Parse(PlayerInfo.instance.playerMoneyText.text) - 10).ToString();
            PlayerInfo.instance.playerPotionText.text = (int.Parse(PlayerInfo.instance.playerPotionText.text) + 1).ToString();
        }
    }
}
