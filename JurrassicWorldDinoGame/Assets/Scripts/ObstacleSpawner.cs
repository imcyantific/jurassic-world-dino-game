using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject groundObstaclePrefab;
    public GameObject flyingObstaclePrefab;
    public float spawnInterval = 2f;
    public float obstacleSpeed = 5f;
    public float spawnX = 10f;
    public float groundSpawnY = -5f;
    public float flyingSpawnY = -6f;  
    public float flyingObstacleChance = 0.3f;

    private float nextSpawnTime = 0f;
    private bool isGameActive = true;


    void Update()
    {
        if (!isGameActive) return;

        if (Time.time >= nextSpawnTime)
        {
            SpawnObstacle();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    void SpawnObstacle()
    {
        bool spawnFlying = Random.value < flyingObstacleChance;
        GameObject obstaclePrefab;
        Vector3 spawnPosition;

        if (spawnFlying && flyingObstaclePrefab != null)
        {
            //spawn flying obstacle
            spawnPosition = new Vector3(spawnX, flyingSpawnY, 0);
            obstaclePrefab = flyingObstaclePrefab;
        }
        else
        {
            //spawn ground obstacle
            spawnPosition = new Vector3(spawnX, groundSpawnY, 0);
            obstaclePrefab = groundObstaclePrefab;
        }

        GameObject obstacle = Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
        obstacle.AddComponent<ObstacleMovement>().speed = obstacleSpeed;
    }

    public void StopSpawning()
    {
        isGameActive = false;
    }

    public void IncreaseSpeed()
    {
        obstacleSpeed += 0.5f;

        spawnInterval = Mathf.Max(1f, spawnInterval - 0.1f);

        // Increase chance of flying obstacles as game gets harder
        flyingObstacleChance = Mathf.Min(0.5f, flyingObstacleChance + 0.05f);
    }
}
