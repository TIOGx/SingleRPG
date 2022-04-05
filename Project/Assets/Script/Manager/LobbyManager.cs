using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    public void GoToGameScene()
    {
        SceneManager.LoadScene("EunjiScene");
    }
    public void GoToLobbyScene()
    {
        SceneManager.LoadScene("LobbyScene");
    }
}
