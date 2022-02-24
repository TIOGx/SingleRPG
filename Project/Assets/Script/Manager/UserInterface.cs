using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
    public static UserInterface instance;
    public Slider ExpBar;


    private void Awake()
    {
        instance = this;
    }

    public void UpdateExpBarUI(float value)
    {

        ExpBar.value = value;
    }
}
