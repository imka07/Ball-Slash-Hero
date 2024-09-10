using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public int score = 0;
    public float comboTimeLimit = 1.5f;
    public int comboMultiplier = 1;
    public TextMeshProUGUI scoreText, comboText;

    public int comboCount = 0;
    private float lastSliceTime = 0f;

    public Animator comboAnimator; 
    public PlayerStats playerStats;

    private void Start()
    {
        UpdateScoreText();
    }

    public void AddScore(int points)
    {
        float currentTime = Time.time;

        if (currentTime - lastSliceTime <= comboTimeLimit)
        {
            comboCount++;
            comboMultiplier = comboCount;

            if (comboCount >= 2) 
            {
                ShowComboText();
            }
        }
        else
        {
            ResetCombo();
        }


        score += points * comboMultiplier;
        lastSliceTime = currentTime;
        UpdateScoreText();

        if (playerStats != null)
        {
            playerStats.CheckNewRecord(score); 
        }
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }

        if (comboText != null)
        {
            comboText.text = "x" + comboCount.ToString();
        }
    }

    private void ShowComboText()
    {
        comboAnimator.SetTrigger("Start");
    }

    public void ResetScore()
    {
        score = 0;
        UpdateScoreText();
    }

    private void ResetCombo()
    {
        if (comboCount >= 2)
        {
            comboAnimator.SetTrigger("End");
        }

        comboCount = 1;
        comboMultiplier = 1;
        lastSliceTime = Time.time; 
    }
}
