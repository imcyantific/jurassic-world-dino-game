using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public float spawnInterval = 2f;
    public float obstacleSpeed = 5f;
    public float spawnX = 10f;
    public float spawnY = -2f;

    private float nextSpawnTime = 0f;
    private bool isGameActive = true;

    void Start()
    {
        
    }

    // Update is called once per frame
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
        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0);
        GameObject obstacle = Instantiate(obstaclePrefab, spawnPosition, Quarternion.identity)

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
    }
}
