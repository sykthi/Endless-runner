using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool isGameStarted;

    public static bool gameOver;
    public GameObject gameOverPanel;

    public static int noOfCoins;

    public TMP_Text coins;
    void Start()
    {
        isGameStarted = false;
        gameOver = false;
        Time.timeScale = 1f;
        noOfCoins = 0;
    }

    void Update()
    {
        if (gameOver)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
        }
        coins.text = "Coins: " + noOfCoins;
    }

    
}
