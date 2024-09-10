using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public GameObject[] ballPrefab;

    public float initialSpawnInterval = 2f; 
    public float minSpawnInterval = 0.5f; 
    public float spawnForce;

    public float minX = -3f;
    public float maxX = 3f;
    public float spawnY = -5f;

    public float difficultyIncreaseInterval = 10f; 
    public float difficultyPhaseDuration = 30f; 
    public float relaxationPhaseDuration = 15f; 

    private float currentSpawnInterval;
    private float timeSinceLastSpawn;
    private float timeSinceLastDifficultyIncrease;
    private bool inDifficultyPhase = true; 

    public bool isSpawning = false;

    private void Start()
    {
        currentSpawnInterval = initialSpawnInterval;
        timeSinceLastSpawn = 0f;
        timeSinceLastDifficultyIncrease = 0f;
    }

    private void Update()
    {
        if (isSpawning)
        {
            timeSinceLastSpawn += Time.deltaTime;
            timeSinceLastDifficultyIncrease += Time.deltaTime;
            spawnForce = Random.Range(9.5f, 12);
            if (timeSinceLastSpawn >= currentSpawnInterval)
            {
                SpawnBall();
                timeSinceLastSpawn = 0f;
            }

            if (inDifficultyPhase)
            {
                if (timeSinceLastDifficultyIncrease >= difficultyIncreaseInterval)
                {
         
                    currentSpawnInterval = Mathf.Max(currentSpawnInterval * 0.9f, minSpawnInterval); 
                    timeSinceLastDifficultyIncrease = 0f;

                
                    if (timeSinceLastDifficultyIncrease >= difficultyPhaseDuration)
                    {
                        inDifficultyPhase = false;
                        timeSinceLastDifficultyIncrease = 0f;
                        currentSpawnInterval = initialSpawnInterval; 
                    }
                }
            }
            else
            {

                if (timeSinceLastDifficultyIncrease >= relaxationPhaseDuration)
                {
                    inDifficultyPhase = true;
                    timeSinceLastDifficultyIncrease = 0f;
                }
            }
        }
    }

    public void StartSpawning()
    {
        isSpawning = true;
    }

    void SpawnBall()
    {
        float randomX = Random.Range(minX, maxX);
        Vector2 spawnPosition = new Vector2(randomX, spawnY);
        var rand = Random.Range(0, ballPrefab.Length);
        GameObject newBall = Instantiate(ballPrefab[rand], spawnPosition, Quaternion.identity);

        Rigidbody2D rb = newBall.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.AddForce(Vector2.up * spawnForce, ForceMode2D.Impulse);
        }
    }
}
