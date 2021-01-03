using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUICanvas : MonoBehaviour
{
    public GameObject pauseButton;
    public GameObject pausePanel;
    public GameObject WinPanel;
    public GameObject LosePanel;
    public GameObject StatisticPanel;
    public GameObject scoreCounter;

    public Text currentScore;
    public Text bestScore;


    void Start()
    {
        if (Game.typeOfDifficulties == Game.TypeOfDifficulties.Speed)
        {
            Game.currestScore = 0;
            scoreCounter.SetActive(true);
            UpdateScorePanel();
        }
    }
    
    public void OnClickExitButton()
    {
        SceneManager.LoadScene(0);
    }

    public void OnClickResumeButton()
    {
        pausePanel.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1;
    }

    public void OnClickRetryOrNewButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void OnclickPauseButton()
    {
        pausePanel.SetActive(true);
        OpenUI();
    }

    public void ShowWonPanel()
    {
        WinPanel.SetActive(true);
        OpenUI();
    }

    public void ShowLosePanel()
    {
        LosePanel.SetActive(true);
        OpenUI();
    }

    public void UpdateScorePanel()
    {
        scoreCounter.transform.GetChild(0).GetComponent<Text>().text = Game.currestScore.ToString();
    }

    public void ShowStatisticPanel()
    {
        currentScore.text = Game.currestScore.ToString();
        bestScore.text = Game.currestScore > Game.bestScore ? Game.currestScore.ToString() : Game.bestScore.ToString();

        StatisticPanel.SetActive(true);
        
        OpenUI();
        scoreCounter.SetActive(false);

        Game.SaveScore();
    }

    private void OpenUI()
    {
        pauseButton.SetActive(false);
        Time.timeScale = 0;
    }
}
