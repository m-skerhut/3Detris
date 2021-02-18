using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonController : MonoBehaviour
{
    [SerializeField]
    private GameObject pausePanel;
   

    public void PlayButtonPressed()
    {
        SceneManager.LoadScene("GameplayScene");
    }

    public void PauseButtonPressed()
    {
        Time.timeScale = 0.0f;
        pausePanel.SetActive(true);
    }

    public void ResumeButtonPressed()
    {
        Time.timeScale = 1.0f;
        pausePanel.SetActive(false);
    }

    public void MainMenuPressed()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenuScene");
    }
}
