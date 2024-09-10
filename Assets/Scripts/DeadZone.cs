using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadZone : MonoBehaviour
{
    public int maxLives = 3; 
    private int currentLives;

    public GameObject[] hearts; 
    public Sprite fullHeart; 
    public Sprite emptyHeart;
    private bool isGameOver = false;
    public GameObject gameOverPanel;
    public Animator comboText;
    private void Start()
    {
        currentLives = maxLives;
        gameOverPanel.SetActive(false);
        UpdateHeartsUI();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball") && collision.GetComponent<BallSplit>().isCut == false)
        {

            Destroy(collision.gameObject);
            currentLives--;
            UpdateHeartsUI();

            if (currentLives <= 0)
            {
                isGameOver = true;
                gameOverPanel.SetActive(true);
                Knife knife = FindObjectOfType<Knife>();
                if (knife != null)
                {
                    knife.DisableKnife(false);
                }

                RemoveAllBalls();
                comboText.SetTrigger("End");
                Time.timeScale = 0;
            }
        }
    }

    public void ResetHearts()
    {
        currentLives = 3;
        UpdateHeartsUI();
    }

    private void UpdateHeartsUI()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentLives)
            {
                hearts[i].GetComponent<UnityEngine.UI.Image>().sprite = fullHeart;
            }
            else
            {
                hearts[i].GetComponent<UnityEngine.UI.Image>().sprite = emptyHeart;
            }
        }
    }

    private void RemoveAllBalls()
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        foreach (GameObject ball in balls)
        {
            Destroy(ball);
        }
    }
}
