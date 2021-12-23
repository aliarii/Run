using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelUIManager : MonoBehaviour
{

    public GameObject startingText;
    public GameObject pausePanel;
    public GameObject pauseButton;
    public GameObject gameOverPanel;
    public Text scoreText;
    public static bool isGameStarted;
    public static bool isGameOver;
    public static bool isGamePause;
    public static int score;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        isGamePause = false;
        isGameOver = false;
        isGameStarted = false;
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver)
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0;
        }
        scoreText.text = "Score: " + score;
        if (SwipeManager.tap && !isGameStarted)
        {
            isGameStarted = true;
            startingText.SetActive(false);
        }


    }
    public void PauseGame()
    {
        if (!isGamePause)
        {
            pauseButton.SetActive(false);
            pausePanel.SetActive(true);
            Time.timeScale = 0;
            isGamePause = true;
        }
        else
        {
            pauseButton.SetActive(true);
            pausePanel.SetActive(false);
            Time.timeScale = 1;
            isGamePause = false;
        }
    }
    public void ReplayGame()
    {
        SceneManager.LoadScene("Level");
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void QuitGame()
    {
        Application.Quit();
    }


}
