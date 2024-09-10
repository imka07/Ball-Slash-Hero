using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public LoadingManager loadingManager;
    public BallSpawner ballSpawner;
    public Text gameOverScore;
    public DeadZone deadZone;
    public ScoreManager scoreManager;

    public void BackToMenu()
    {
        RemoveAllBalls();
        deadZone.ResetHearts();
        scoreManager.ResetScore();
        Time.timeScale = 1;
        loadingManager.StartLoading();
        ballSpawner.isSpawning = false;
    }

    public void Update()
    {
        var scoreManager = FindObjectOfType<ScoreManager>();
        gameOverScore.text = scoreManager.score.ToString();
    }

    private void RemoveAllBalls()
    {
        // Ищем все объекты с тегом "Ball" и удаляем их
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        foreach (GameObject ball in balls)
        {
            Destroy(ball);
        }
    }
}
