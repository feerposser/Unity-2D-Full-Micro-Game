using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public Text healthText;
    public Text scoreText;
    public static GameController instance;
    public int coins = 0;

    public GameObject gameOver;
    private bool isGameOver = false;
    public GameObject pause;
    private bool isPaused = false;

    void Start()
    {
        instance = this;
        
        if (PlayerPrefs.GetInt("coins") > 0)
        {
            SetCoin(coins);
        }

        Debug.Log(coins);
    }

    private void Update()
    {
        PauseGame();
    }

    public void SetNumberHealth(int health)
    {
        healthText.text = "x " + health.ToString();
    }

    public void SetCoin(int coinValue)
    {
        coins += coinValue;
        scoreText.text = "x " + coins.ToString();
        PlayerPrefs.SetInt("coins", coins);
    }

    private void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            pause.SetActive(isPaused);
        }

        if (isPaused)
        {
            Time.timeScale = 0;
        } else
        {
            if (!isGameOver)
            {
                Time.timeScale = 1;
            }
        }
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
        Time.timeScale = 0;
        isGameOver = true;
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

}
