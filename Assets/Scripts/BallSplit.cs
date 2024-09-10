using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSplit : MonoBehaviour
{
    public Rigidbody2D part1;
    public Rigidbody2D part2;

    public int points = 1; // Очки за разрез одного мяча

    public float maxForce = 10f;
    public float maxTorque = 5f;

    public bool isCut = false;

    public void SplitBall()
    {

        // Проверяем, разрезан ли уже мяч
        if (isCut) return;

        // Устанавливаем флаг, чтобы мяч нельзя было разрезать повторно
        isCut = true;


        part1.isKinematic = false;
        part2.isKinematic = false;

        Vector2 randomDirection1 = Random.insideUnitCircle.normalized;
        Vector2 randomDirection2 = Random.insideUnitCircle.normalized;

        float randomForce1 = Random.Range(0.5f * maxForce, maxForce);
        float randomForce2 = Random.Range(0.5f * maxForce, maxForce);

        part1.AddForce(randomDirection1 * randomForce1, ForceMode2D.Impulse);
        part2.AddForce(randomDirection2 * randomForce2, ForceMode2D.Impulse);

        float randomTorque1 = Random.Range(-maxTorque, maxTorque);
        float randomTorque2 = Random.Range(-maxTorque, maxTorque);

        part1.AddTorque(randomTorque1, ForceMode2D.Impulse);
        part2.AddTorque(randomTorque2, ForceMode2D.Impulse);


        ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
        if (scoreManager != null)
        {
            scoreManager.AddScore(points);
        }

        Destroy(this.gameObject, 1.5f);
    }

    
}
