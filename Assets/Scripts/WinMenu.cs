using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject androidUI;
    public GameObject winMenuUI;
    public TMPro.TextMeshProUGUI winText;
    

    public void WinPause(string playerName)
    {
        winText.text = playerName + " WIN !";
        winMenuUI.SetActive(true);
        androidUI.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
