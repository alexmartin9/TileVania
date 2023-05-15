using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    int currentPoints;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI coinsText;

    void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        livesText.text = playerLives.ToString();
        currentPoints = 0;
        coinsText.text = currentPoints.ToString();

    }

    public void PlayerDeath()
    {
        if (playerLives > 1)
        {
            TakeLive();
        }
        else
        {
            ResetGame();
        }
    }

    void TakeLive()
    {
        playerLives--;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        livesText.text = playerLives.ToString();
    }

    void ResetGame()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);

        ScenePersist scenePersist = FindObjectOfType<ScenePersist>();
        scenePersist.ResetScenePersist();
    }

    public void AddPoints(int points)
    {
        currentPoints += points;
        coinsText.text = currentPoints.ToString();
    }

}
